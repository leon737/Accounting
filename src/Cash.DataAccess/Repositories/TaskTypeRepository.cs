using System.Collections.Generic;
using Functional.Fluent.Extensions;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;

namespace Cash.DataAccess.Repositories
{
    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly DataContext _context;

        public TaskTypeRepository(DataContext context)
        {
            _context = context;
        }

        public IReadOnlyList<TaskType> All() => _context.TaskTypes.AsReadOnlyList();
    }
}