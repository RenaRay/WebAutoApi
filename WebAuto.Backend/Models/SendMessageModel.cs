using System.ComponentModel.DataAnnotations;
using WebAuto.Backend.Properties;

namespace WebAuto.Backend.Models
{
    public class SendMessageModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationMessageToPlateRequired",
            AllowEmptyStrings = false)]
        [StringLength(
            10,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationMessageToPlateStringLength")]
        public string ToPlate { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationMessageTextRequired",
            AllowEmptyStrings = false)]
        [StringLength(
            256,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationMessageTextStringLength")]
        public string Text { get; set; }
    }
}