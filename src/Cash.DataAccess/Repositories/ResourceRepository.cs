using System;
using System.Collections.Generic;
using System.Data.Entity;
using Functional.Fluent;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using System.Linq;

namespace Cash.DataAccess.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly DataContext _context;

        public ResourceRepository(DataContext context)
        {
            _context = context;
        }

        public Result<Resource> ById(int id) => Result.SuccessIfNotNull(_context.Resources.Find(id));

        public IReadOnlyList<Resource> All(Project project) => _context.Resources.Where(x => x.Project.Id == project.Id).AsReadOnlyList();
        
        public IReadOnlyList<Resource> ByPrefix(string term, Project project) => 
            _context.Resources.Where(x => x.Name.ToLower().StartsWith(term.ToLower()) && x.Project.Id == project.Id).AsReadOnlyList();

        public Result<Resource> Add(Resource resource, User creator, Project project)
        {
            resource.Created = DateTime.Now;
            resource.CreatedBy = creator.Id;
            resource.Modified = null;
            resource.ProjectId = project.Id;
            _context.Resources.Add(resource);
            return Result.Success(resource);
        }

        public Result<Resource> Update(Resource resource, User modifier)
        {
            resource.Modified = DateTime.Now;
            resource.ModifiedBy = modifier.Id;
            _context.Entry(resource).State = EntityState.Modified;
            _context.Entry(resource).Property(x => x.Created).IsModified = false;
            _context.Entry(resource).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(resource).Property(x => x.ProjectId).IsModified = false;
            return Result.Success(resource);
        }

        public Result<Unit> Delete(int id) =>
            ById(id).Success(v =>
            {
                _context.Resources.Remove(v);
                return Result.Success();
            });
    }
}