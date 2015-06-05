using System.Linq;
using WebAuto.Backend.Models;
using WebAuto.DataAccess;

namespace WebAuto.Backend.Extensions
{
    public static class UserExtensions
    {
        public static UserProfileModel ToUserProfileModel(this User user, string currentUserLogin)
        {
            if (user == null)
            {
                return null;
            }

            var cars = user.Cars ?? Enumerable.Empty<Car>();
            var profile =
                new UserProfileModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Avatar = user.AvatarId,
                    Cars = cars.Select(car => car.ToModel()).ToList()
                };
            
            if (currentUserLogin == user.Login)
            {
                profile.Phone = user.Phone;
                profile.Email = user.Email;
            }
            return profile;
        }
    }
}