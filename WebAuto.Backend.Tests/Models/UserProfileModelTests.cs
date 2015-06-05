using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebAuto.Backend.Models;
using WebAuto.Backend.Tests.Helpers;

namespace WebAuto.Backend.Tests.Controllers
{
    [TestFixture]
    public class UserProfileModelTests
    {
        [Test]
        [TestCaseSource("ValidUserProfileModels")]
        public void Validate_ModelIsValid_ValidationResultIsEmpty(UserProfileModelTestCase testCase)
        {
            var model = testCase.Model;
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);

            Assert.AreEqual(0, validationResults.Count, string.Join("\r\n", validationResults));
        }

        public static IEnumerable<UserProfileModelTestCase> ValidUserProfileModels
        {
            get
            {
                var modelWithoutEmailAndPhone =
                    new UserProfileModel
                    {
                        FirstName = "Ivan",
                        LastName = "Ivanov"
                    };
                yield return
                    new UserProfileModelTestCase
                    {
                        Model = modelWithoutEmailAndPhone,
                        Description = "without email and phone"
                    };

                var modelWithEmailAndPhone = modelWithoutEmailAndPhone.Clone();
                modelWithEmailAndPhone.Email = "ivan.ivanov@gmail.com";
                modelWithEmailAndPhone.Phone = "+79179876543";
                yield return
                    new UserProfileModelTestCase
                    {
                        Model = modelWithEmailAndPhone,
                        Description = "with email and phone"
                    };

                var modelWithEmptyEmailAndPhone = modelWithoutEmailAndPhone.Clone();
                modelWithEmptyEmailAndPhone.Email = string.Empty;
                modelWithEmptyEmailAndPhone.Phone = string.Empty;
                yield return
                    new UserProfileModelTestCase
                    {
                        Model = modelWithEmptyEmailAndPhone,
                        Description = "with empty email and phone"
                    };
            }
        }

        

        public class UserProfileModelTestCase
        {
            public UserProfileModel Model { get; set; }
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
        [TestCaseSource("InvalidUserProfileModels")]
        public void Validate_ModelIsInvalid_ValidationResultIsNotEmpty(UserProfileModelTestCase testCase)
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

        public static IEnumerable<UserProfileModelTestCase> InvalidUserProfileModels
        {
            get
            {
                var longFirstName = ValidUserProfileModels.First();
                longFirstName.Model.FirstName = new string('a', 65);
                longFirstName.Description = "max+1 first name length";
                yield return longFirstName;

                var longLastName = ValidUserProfileModels.First();
                longLastName.Model.LastName = new string('a', 65);
                longLastName.Description = "max+1 last name length";
                yield return longLastName;

                var nullFirstName = ValidUserProfileModels.First();
                nullFirstName.Model.FirstName = null;
                nullFirstName.Description = "first name is null";
                yield return nullFirstName;

                var nullLastName = ValidUserProfileModels.First();
                nullLastName.Model.LastName = null;
                nullLastName.Description = "last name is null";
                yield return nullLastName;

                var longEmail = ValidUserProfileModels.First();
                longEmail.Model.Email = new string('a', 65);
                longEmail.Description = "max+1 email length";
                yield return longEmail;

                var longPhone = ValidUserProfileModels.First();
                longPhone.Model.Phone = new string('a', 13);
                longPhone.Description = "max+1 phone length";
                yield return longPhone;
            }
        }
    }
}
