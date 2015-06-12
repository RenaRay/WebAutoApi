using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAuto.Backend.Properties;

namespace WebAuto.Backend.Models
{
    public class UserProfileModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationFirstNameRequired",
            AllowEmptyStrings = false)]
        [StringLength(
            64,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationFirstNameStringLength")]
        public string FirstName { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationLastNameRequired",
            AllowEmptyStrings = false)]
        [StringLength(
            64,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationLastNameStringLength")]
        public string LastName { get; set; }

        [StringLength(
            64,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationEmailStringLength")]
        public string Email { get; set; }

        public string Avatar { get; set; }

        public List<CarModel> Cars { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? FirstLicenseDate { get; set; }

        public char MaritalStatus { get; set; }

        public string Occupation { get; set; }

        public DateTime? BirthDate { get; set; }

        public char Gender { get; set; }

        public string HairColor { get; set; }
    }
}