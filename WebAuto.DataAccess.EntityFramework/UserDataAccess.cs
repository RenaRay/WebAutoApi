using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace WebAuto.DataAccess.EntityFramework
{
    public class UserDataAccess : IUserDataAccess
    {
        public Task<User> FindByLoginAsync(string login)
        {
            return FindFirstOrDefault(co => co.Login == login);
        }

        private static async Task<User> FindFirstOrDefault(Expression<Func<CarOwner, bool>> predicate)
        {
            CarOwner carOwner;
            using (var entities = new Entities())
            {
                carOwner = await entities.CarOwner.FirstOrDefaultAsync(predicate);
            }
            if (carOwner == null)
            {
                return null;
            }

            var user = GetUserFromCarOwner(carOwner);
            return user;
        }

        private static User GetUserFromCarOwner(CarOwner carOwner)
        {
            var user =
                new User
                {
                    Id = carOwner.CarOwnerID,
                    Login = carOwner.Login,
                    PasswordHash = carOwner.Password,
                    Email = carOwner.Email,
                    FirstName = carOwner.FirstName,
                    LastName = carOwner.LastName,
                };
            return user;
        }

        public Task<User> FindByLoginAndPasswordHashAsync(string login, string passwordHash)
        {
            return FindFirstOrDefault(co =>
                co.Login == login &&
                co.Password == passwordHash);
        }

        public Task<User> FindByPlateExactAsync(string plate)
        {
            //TODO: нужно искать без учета регистра
            return FindFirstOrDefault(co => co.Car.Any(car => car.RegNumber == plate));
        }

        public async Task CreateAsync(User user)
        {
            var carOwner = GetCarOwnerFromUser(user);
            using (var entities = new Entities())
            {
                entities.CarOwner.Add(carOwner);
                await entities.SaveChangesAsync();
            }
        }

        private static CarOwner GetCarOwnerFromUser(User user)
        {
            var carOwner =
                new CarOwner
                {
                    CarOwnerID = user.Id,
                    Login = user.Login,
                    Password = user.PasswordHash,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
            return carOwner;
        }

        public async Task UpdateAsync(User user)
        {
            var carOwner = GetCarOwnerFromUser(user);
            using (var entities = new Entities())
            {
                entities.CarOwner.Attach(carOwner);
                entities.Entry(carOwner).State = EntityState.Modified;
                await entities.SaveChangesAsync();
            }
        }
    }
}
