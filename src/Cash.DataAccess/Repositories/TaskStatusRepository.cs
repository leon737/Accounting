using System.Collections.Generic;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;

namespace Cash.DataAccess.Repositories
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly DataContext _context;

        public TaskStatusRepository(DataContext context)
        {
            _context = context;
        }

        public Result<TaskStatus> ById(int id) => Result.SuccessIfNotNull(_context.TaskStatuses.Find(id));

        public IReadOnlyList<TaskStatus> All() => _context.TaskStatuses.AsReadOnlyList();
    }
}