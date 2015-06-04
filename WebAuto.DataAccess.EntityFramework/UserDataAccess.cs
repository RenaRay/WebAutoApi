using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAuto.DataAccess.EntityFramework
{
    public class UserDataAccess : IUserDataAccess
    {
        private List<User> _users = new List<User>
        {
            new User
            {
                Login = "test",
                PasswordHash = "n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=",

            }
        };

        public Task<User> FindByLoginAsync(string login)
        {
            var user = _users.FirstOrDefault(u => u.Login == login);
            return Task.FromResult(user);
        }

        public Task<User> FindByLoginAndPasswordHashAsync(string login, string passwordHash)
        {
            var user = _users.FirstOrDefault(u => u.Login == login && u.PasswordHash == passwordHash);
            return Task.FromResult(user);
        }

        public Task<List<User>> FindByIdsAsync(string[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FindByPlatePartAsync(string plate, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FindByPlateExactAsync(string plate)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _users.Add(user);
            return Task.FromResult<Object>(null);
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
