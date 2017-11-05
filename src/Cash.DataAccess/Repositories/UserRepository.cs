using System;
using System.Linq;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;

namespace Cash.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public Result<User> ById(Guid id) => Result.SuccessIfNotNull(_context.Users.Find(id));

        public Result<User> ByUserName(string userName) =>
            Result.SuccessIfNotNull(_context.Users.FirstOrDefault(x => x.UserName == userName));
    }
}