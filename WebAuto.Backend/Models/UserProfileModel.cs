using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAuto.Backend.Properties;
using WebAuto.Common;

namespace WebAuto.Backend.Models
{
    public class UserProfileModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationFirstNameRequired",
            AllowEmptyStrings=false)]
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

        [StringLength(
            12,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationPhoneStringLength")]
        public string Phone { get; set; }

        public ContactsVisibility ContactsVisibleTo { get; set; }

        public int? Avatar { get; set; }

        public List<CarModel> Cars { get; set; }
    }
}