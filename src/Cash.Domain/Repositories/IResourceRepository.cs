using System.Collections.Generic;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;

namespace Cash.Domain.Repositories
{
    public interface IResourceRepository
    {
        Result<Resource> ById(int id);

        IReadOnlyList<Resource> All(Project project);

        IReadOnlyList<Resource> ByPrefix(string term, Project project);
            
        Result<Resource> Add(Resource resource, User creator, Project project);

        Result<Resource> Update(Resource resource, User modifier);

        Result<Unit> Delete(int id);
    }
}