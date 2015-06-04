using WebAuto.Backend.Models;

namespace WebAuto.Backend.Tests.Helpers
{
    public static class UserRegistrationModelExtensions
    {
        public static UserRegistrationModel Clone(this UserRegistrationModel model)
        {
            var clone =
                new UserRegistrationModel
                {
                    Login = model.Login,
                    Password = model.Password,
                    PasswordConfirmation = model.PasswordConfirmation
                };
            return clone;
        }
    }
}
