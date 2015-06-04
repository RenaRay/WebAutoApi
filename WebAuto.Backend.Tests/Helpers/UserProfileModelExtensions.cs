using System.Collections.Generic;
using WebAuto.Backend.Models;

namespace WebAuto.Backend.Tests.Helpers
{
    public static class UserProfileModelExtensions
    {
        public static UserProfileModel Clone(this UserProfileModel model)
        {
            var clone =
                new UserProfileModel
                {
                    ContactsVisibleTo = model.ContactsVisibleTo,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Cars = model.Cars != null ? new List<CarModel>(model.Cars) : new List<CarModel>(),
                    Avatar = model.Avatar
                };
            return clone;
        }
    }
}
