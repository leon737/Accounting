using System;
using System.Collections.Generic;
using System.Data.Entity;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using System.Linq;

namespace Cash.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public Result<Task> ById(int id) => Result.SuccessIfNotNull(_context.Tasks.Include(v => v.Resources).FirstOrDefault(x => x.Id == id));

        public IReadOnlyList<Task> All(int? parentTaskId, Project project) => _context.Tasks.Where(x => x.ParentId == parentTaskId && x.ProjectId == project.Id).AsReadOnlyList();

        public Result<Task> Add(Task task, User creator, Project project)
        {
            task.Id = 0;
            task.Created = DateTime.Now;
            task.CreatedBy = creator.Id;
            task.Modified = null;
            task.ProjectId = project.Id;
            task.TaskStatusId = 1; // new
            _context.Tasks.Add(task);
            return Result.Success(task);
        }

        public Result<Task> Update(Task task, User modifier, bool updateStatus = false)
        {
            task.Modified = DateTime.Now;
            task.ModifiedBy = modifier.Id;
            _context.Entry(task).State = EntityState.Modified;
            _context.Entry(task).Property(x => x.Created).IsModified = false;
            _context.Entry(task).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(task).Property(x => x.ProjectId).IsModified = false;
            if (!updateStatus)
                _context.Entry(task).Property(x => x.TaskStatusId).IsModified = false;
            return Result.Success(task);
        }

        public Result<int?> Delete(int id) => 
            ById(id).Success(v =>
            {
                _context.Tasks.Remove(v);
                return Result.Success(v.ParentId);
            });

        public IReadOnlyList<Task> ByPrefix(string term, Project project)
        {
            return GetActiveTasksByProject(project).Where(x => x.Id.ToString() == term)
                .Concat(GetActiveTasksByProject(project).Where(x => x.Name.ToLower().StartsWith(term.ToLower())))
                .AsReadOnlyList();
        }
        
        private IQueryable<Task> GetActiveTasksByProject(Project project) => _context.Tasks.Where(x => x.Active && x.ProjectId == project.Id);
    }
}