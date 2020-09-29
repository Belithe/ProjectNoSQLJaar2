using Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private string collection = "Users";

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public void AddUser(User user)
        {
            _repo.Add(user, collection);
        }

        public long CountThemAll()
        {
            return _repo.Count(collection);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _repo.GetAll(collection);
        }

        public User GetSingle(string id)
        {
            return _repo.GetSingle(id, collection);
        }

        public User GetSingleWherePredicate(Expression<Func<User, bool>> predicate)
        {
            return _repo.GetSingleItemPredicate(predicate, collection);
        }

        public void RemoveUser(User user)
        {
            _repo.Delete(user, collection);
        }

        public void UpdateUser(User user)
        {
            _repo.Update(user, collection);
        }
    }
}
