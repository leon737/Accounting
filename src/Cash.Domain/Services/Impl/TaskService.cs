using System;
using System.Collections.Generic;
using System.Linq;
using Functional.Fluent;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;
using Cash.Domain.Repositories;

namespace Cash.Domain.Services.Impl
{
    public class TaskService : ITaskService
    {

        private readonly ITaskResourceRepository _taskResourceRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskStatusRepository _taskStatusRepository;

        public TaskService(ITaskResourceRepository taskResourceRepository, ITaskRepository taskRepository, ITaskStatusRepository taskStatusRepository)
        {
            _taskResourceRepository = taskResourceRepository;
            _taskRepository = taskRepository;
            _taskStatusRepository = taskStatusRepository;
        }

        public Result<Unit> SetTaskResources(Task task, ICollection<TaskResource> resources, User creator)
        {
            _taskRepository.ById(task.Id).SuccessValue(t => _taskResourceRepository.RemoveByTask(t));

            foreach (var taskResource in MergeResources(resources))
            {
                taskResource.TaskId = task.Id;
                _taskResourceRepository.Add(taskResource, creator);
            }
            return Result.Success();
        }

        public Result<int> UpdateTaskImportance(Task task, SetImportanceActions action, User modifier)
        {
            task.Importance += action.Match()
                .With(SetImportanceActions.Up, 1)
                .With(SetImportanceActions.Down, -1)
                .ElseThrow<ArgumentException>()
                .Do();
            return _taskRepository.Update(task, modifier)
                .SuccessValue(task.Importance);
        }

        public Result<ICollection<TaskStatusTransition>> SetStatus(Task task, int targetStatusId, User modifier)
        {
            if (task.TaskStatus.Transitions.All(x => x.TargetStatusId != targetStatusId))
                return Result.Fail<ICollection<TaskStatusTransition>>();
            task.TaskStatusId = targetStatusId;
            _taskRepository.Update(task, modifier, true);
            return Result.Success(_taskStatusRepository.ById(targetStatusId).Value.Transitions);
        }

        private ICollection<TaskResource> MergeResources(ICollection<TaskResource> resources)
        {
            var result = new List<TaskResource>();
            foreach (var taskResource in resources)
            {
                var element = result.SingleOrDefault(x => x.ResourceId == taskResource.ResourceId);
                if (element != null)
                    element.Quantity += taskResource.Quantity;
                else
                    result.Add(taskResource);
            }
            return result;
        }
    }
}