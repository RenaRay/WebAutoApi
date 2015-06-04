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
                    ContactsVisibleTo = user.ContactsVisibleTo,
                    Avatar = user.AvatarId,
                    Cars = cars.Select(car => car.ToModel()).ToList()
                };
            //TODO: when friends will be implemented, add condition for ContactsVisibility.Friends
            if (currentUserLogin == user.Login ||
                user.ContactsVisibleTo == Common.ContactsVisibility.Everyone)
            {
                profile.Phone = user.Phone;
                profile.Email = user.Email;
            }
            return profile;
        }
    }
}