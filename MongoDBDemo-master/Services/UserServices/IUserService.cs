using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq.Expressions;

namespace Services.UserServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        long CountThemAll();

        User GetSingle(string id);

        User GetSingleWherePredicate(Expression<Func<User, bool>> predicate);

        void AddUser(User user);

        void UpdateUser(User user);

        void RemoveUser(User user);
    }
}
