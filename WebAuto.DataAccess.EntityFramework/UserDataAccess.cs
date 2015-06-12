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
                if (carOwner == null)
                {
                    return null;
                }

                var user = GetUserFromCarOwner(carOwner);
                return user;
            }
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
                    Cars = carOwner.Car.Select(c =>
                        new WebAuto.DataAccess.Car
                        {
                            Id = c.CarID,
                            Model = c.Model,
                            Vendor = c.Brand,
                            Plate = c.RegNumber
                        })
                        .ToList()
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
            var carOwner = new CarOwner();
            UpdateCarOwnerFromUser(carOwner, user);
            return carOwner;
        }

        private static void UpdateCarOwnerFromUser(CarOwner carOwner, User user)
        {
            carOwner.CarOwnerID = user.Id;
            carOwner.Login = user.Login;
            carOwner.Password = user.PasswordHash;
            carOwner.Email = user.Email;
            carOwner.FirstName = user.FirstName;
            carOwner.LastName = user.LastName;
            
            if (carOwner.Car == null)
            {
                carOwner.Car = new List<Car>();
            }
            //обновляем автомобили(для каждого авто CarOwner'а: находим у user'а автомобиль по идентификатору авто CarOwner'а и перезаписываем все свойства авто)
            for (var i = 0; i < carOwner.Car.Count; i++ )
            {
                var carOwnerCar = carOwner.Car.ElementAt(i);
                var userCar = user.Cars.FirstOrDefault(c => c.Id == carOwnerCar.CarID);
                if (userCar != null)
                {
                    //перезаписать все свойства
                    UpdateCarOwnerCarFromUserCar(carOwnerCar, userCar);
                }
                else//удаляем автомобили
                {
                    carOwner.Car.Remove(carOwnerCar);
                }
            }
            
            //добавляем CarOwner'у все автомобили user'а, у которых нет идентификатора(<1)
            foreach(var userCar in user.Cars.Where(c => c.Id < 1))
            {
                var carOwnerCar = new Car();
                UpdateCarOwnerCarFromUserCar(carOwnerCar, userCar);
                carOwner.Car.Add(carOwnerCar);
            }
        }

        private static void UpdateCarOwnerCarFromUserCar(Car carOwnerCar, DataAccess.Car userCar)
        {
            carOwnerCar.Model = userCar.Model;
            carOwnerCar.Brand = userCar.Vendor;
            carOwnerCar.RegNumber = userCar.Plate;
        }
        
        public async Task UpdateAsync(User user)
        {
            using (var entities = new Entities())
            {
                //1. получить пользователя из базы по id
                var carOwner = entities.CarOwner.FirstOrDefault(co => co.CarOwnerID == user.Id);
                //2. заполнить все свойства(в том числе и авто) пользователя из базы значениями из параметра user
                UpdateCarOwnerFromUser(carOwner, user);

                await entities.SaveChangesAsync();
            }
        }
    }
}
