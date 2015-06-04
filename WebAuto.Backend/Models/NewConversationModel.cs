using System.ComponentModel.DataAnnotations;
using WebAuto.Backend.Properties;

namespace WebAuto.Backend.Models
{
    public class NewConversationModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationConversationFromPlateRequired",
            AllowEmptyStrings = false)]
        [StringLength(
            10,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationConversationFromPlateStringLength")]
        public string FromPlate { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationConversationToPlateRequired",
            AllowEmptyStrings = false)]
        [StringLength(
            10,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationConversationToPlateStringLength")]
        public string ToPlate { get; set; }

        public string ToUser { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationConversationMessageRequired",
            AllowEmptyStrings = false)]
        [StringLength(
            256,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationConversationMessageStringLength")]
        public string Message { get; set; }
    }
}