using System.ComponentModel.DataAnnotations;
using WebAuto.Backend.Properties;
using WebAuto.Common;

namespace WebAuto.Backend.Models
{
    public class UserRegistrationModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationLoginRequired")]
        [StringLength(
            16,
            MinimumLength = 6,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationLoginStringLength")]
        public string Login { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationPasswordRequired")]
        [StringLength(
            16,
            MinimumLength = 6,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationPasswordStringLength")]
        public string Password { get; set; }

        [Compare(
            "Password",
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationPasswordConfirmationCompare")]
        public string PasswordConfirmation { get; set; }
    }
}