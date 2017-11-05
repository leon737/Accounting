using System.Collections.Generic;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;

namespace Cash.Domain.Repositories
{
    public interface IProjectRepository
    {
        Result<Project> ById(int id);

        Result<Project> ByCode(string code);
            
        IReadOnlyList<Project> All();

        Result<Project> Add(Project project, User creator);

        Result<Project> Update(Project project, User modifier);

        /// <summary> deletes the project </summary>
        Result<Unit> Delete(int id);
    }
}