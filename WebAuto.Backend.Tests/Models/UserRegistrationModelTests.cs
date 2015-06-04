using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebAuto.Backend.Models;

namespace WebAuto.Backend.Tests.Controllers
{
    [TestFixture]
    public class UserRegistrationModelTests
    {
        [Test]
        [TestCaseSource("ValidUserRegistrationModels")]
        public void Validate_ModelIsValid_ValidationResultIsEmpty(UserRegistrationModelTestCase testCase)
        {
            var model = testCase.Model;
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);

            Assert.AreEqual(0, validationResults.Count, string.Join("\r\n", validationResults));
        }

        public static IEnumerable<UserRegistrationModelTestCase> ValidUserRegistrationModels
        {
            get
            {
                var modelWithLoginAndPassword =
                    new UserRegistrationModel
                    {
                        Login = "ivan.ivanov",
                        Password = "qwerty123",
                        PasswordConfirmation = "qwerty123"
                    };
                yield return
                    new UserRegistrationModelTestCase
                    {
                        Model = modelWithLoginAndPassword,
                        Description = "with login and password"
                    };
            }
        }

        

        public class UserRegistrationModelTestCase
        {
            public UserRegistrationModel Model { get; set; }
            public string Description { get; set; }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return base.ToString();
                }
                return Description;
            }
        }

        [Test]
        [TestCaseSource("InvalidUserRegistrationModels")]
        public void Validate_ModelIsInvalid_ValidationResultIsNotEmpty(UserRegistrationModelTestCase testCase)
        {
            var model = testCase.Model;
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);

            Assert.AreEqual(
                1,
                validationResults.Count,
                "Validation result should contain one error for invalid model. Actual validation result: \r\n" +
                    string.Join("\r\n", validationResults));
        }

        public static IEnumerable<UserRegistrationModelTestCase> InvalidUserRegistrationModels
        {
            get
            {
                var shortLogin = ValidUserRegistrationModels.First();
                shortLogin.Model.Login = "test1";
                shortLogin.Description = "min-1 login length";
                yield return shortLogin;

                var shortPassword = ValidUserRegistrationModels.First();
                shortPassword.Model.Password = "test1";
                shortPassword.Model.PasswordConfirmation = "test1";
                shortPassword.Description = "min-1 password length";
                yield return shortPassword;

                var longLogin = ValidUserRegistrationModels.First();
                longLogin.Model.Login = "abcdefghijklmnopq";
                longLogin.Description = "max+1 login length";
                yield return longLogin;

                var longPassword = ValidUserRegistrationModels.First();
                longPassword.Model.Password = "qponmlkjihgfedcba";
                longPassword.Model.PasswordConfirmation = "qponmlkjihgfedcba";
                longPassword.Description = "max+1 password length";
                yield return longPassword;

                var nullLogin = ValidUserRegistrationModels.First();
                nullLogin.Model.Login = null;
                nullLogin.Description = "login is null";
                yield return nullLogin;

                var nullPassword = ValidUserRegistrationModels.First();
                nullPassword.Model.Password = null;
                nullPassword.Model.PasswordConfirmation = null;
                nullPassword.Description = "password is null";
                yield return nullPassword;

                var passwordConfirmationDoesnMatch = ValidUserRegistrationModels.First();
                passwordConfirmationDoesnMatch.Model.PasswordConfirmation =
                    new string(
                        passwordConfirmationDoesnMatch.Model.PasswordConfirmation
                            .Reverse()
                            .ToArray());
                passwordConfirmationDoesnMatch.Description = "password doesn't match confirmation";
                yield return passwordConfirmationDoesnMatch;
            }
        }
    }
}
