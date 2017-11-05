using System.Collections.Generic;
using Cash.Domain.Models;

namespace Cash.Domain.Repositories
{
    public interface ITaskTypeRepository
    {
        IReadOnlyList<TaskType> All();
    }
}