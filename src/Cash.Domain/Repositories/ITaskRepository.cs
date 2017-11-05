using System.Collections.Generic;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;

namespace Cash.Domain.Repositories
{
    public interface ITaskRepository
    {
        Result<Task> ById(int id);

        IReadOnlyList<Task> All(int? parentTaskId, Project project);

        Result<Task> Add(Task task, User creator, Project project);

        Result<Task> Update(Task task, User modifier, bool updateStatus = false);

        /// <summary> deletes the task </summary>
        /// <returns>parent task id, if any</returns>
        Result<int?> Delete(int id);

        IReadOnlyList<Task> ByPrefix(string term, Project project);

    }
}