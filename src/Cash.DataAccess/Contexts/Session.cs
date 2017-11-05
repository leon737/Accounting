using Functional.Fluent;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Contexts;

namespace Cash.DataAccess.Contexts
{
    public class Session : ISession
    {
        private readonly DataContext _context;

        public Session(DataContext context)
        {
            _context = context;
        }

        /// <summary> Saves changes into data storage </summary>
        public Result<Unit> SaveChanges()
        {
            _context.SaveChanges();
            return Result.Success();
        }
    }

}