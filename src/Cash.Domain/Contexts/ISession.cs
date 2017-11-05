using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Contexts
{
    public interface ISession
    {
        /// <summary> Saves changes into data storage </summary>
        Result<Unit> SaveChanges();
    }
}