using System;
using System.Linq;
using Functional.Fluent;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;

namespace Cash.DataAccess.Repositories
{
    public class TaskResourceRepository : ITaskResourceRepository
    {

        private readonly DataContext _context;

        public TaskResourceRepository(DataContext context)
        {
            _context = context;
        }

        public Result<TaskResource> Add(TaskResource taskResource, User creator)
        {
            taskResource.Created = DateTime.Now;
            taskResource.CreatedBy = creator.Id;
            taskResource.Modified = null;
            taskResource.ModifiedBy = null;
            _context.TaskResources.Add(taskResource);
            return Result.Success(taskResource);
        }

        public Result<Unit> RemoveByTask(Task task)
        {
            var resources = task.Resources;
            _context.TaskResources.RemoveRange(resources);
            return Result.Success();
        }

        public bool UsedInTasks(int resourceId) => _context.TaskResources.Any(x => x.ResourceId == resourceId);
    }
}