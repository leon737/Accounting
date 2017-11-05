using System;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;

namespace Cash.Domain.Repositories
{
    public interface ITaskResourceRepository
    {

        Result<TaskResource> Add(TaskResource taskResource, User creator);

        Result<Unit> RemoveByTask(Task task);

        bool UsedInTasks(int resourceId);
    }
}