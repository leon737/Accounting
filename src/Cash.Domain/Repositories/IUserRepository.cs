using System;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;

namespace Cash.Domain.Repositories
{
    public interface IUserRepository
    {
        Result<User> ById(Guid id);

        Result<User> ByUserName(string userName);
    }
}