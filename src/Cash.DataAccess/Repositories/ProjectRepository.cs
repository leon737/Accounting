using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Functional.Fluent;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;

namespace Cash.DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public Result<Project> ById(int id) => Result.SuccessIfNotNull(_context.Projects.Find(id));

        public Result<Project> ByCode(string code) => Result.SuccessIfNotNull(_context.Projects.SingleOrDefault(x => x.Code == code));

        public IReadOnlyList<Project> All() => _context.Projects.AsReadOnlyList();
        
        public Result<Project> Add(Project project, User creator)
        {
            project.Created = DateTime.Now;
            project.CreatedBy = creator.Id;
            project.Modified = null;
            _context.Projects.Add(project);
            return Result.Success(project);
        }

        public Result<Project> Update(Project project, User modifier)
        {
            project.Modified = DateTime.Now;
            project.ModifiedBy = modifier.Id;
            _context.Entry(project).State = EntityState.Modified;
            _context.Entry(project).Property(x => x.Created).IsModified = false;
            _context.Entry(project).Property(x => x.CreatedBy).IsModified = false;
            return Result.Success(project);
        }

        public Result<Unit> Delete(int id) =>
            ById(id).Success(v =>
            {
                _context.Projects.Remove(v);
                return Result.Success();
            });
    }
}