using System.Collections.Generic;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;

namespace Cash.Domain.Repositories
{
    public interface ITaskStatusRepository
    {
        Result<TaskStatus> ById(int id);

        IReadOnlyList<TaskStatus> All();
    }
}