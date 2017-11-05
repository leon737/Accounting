using System.Collections.Generic;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;

namespace Cash.Domain.Services
{
    public interface ITaskService
    {
        Result<Unit> SetTaskResources(Task task, ICollection<TaskResource> resources, User creator);

        Result<int> UpdateTaskImportance(Task task, SetImportanceActions action, User modifier);

        Result<ICollection<TaskStatusTransition>> SetStatus(Task task, int targetStatusId, User modifier);
    }
}