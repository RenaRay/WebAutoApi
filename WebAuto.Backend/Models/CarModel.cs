﻿using System.ComponentModel.DataAnnotations;
using WebAuto.Backend.Properties;

namespace WebAuto.Backend.Models
{
    public class CarModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationCarPlateRequired",
            AllowEmptyStrings = false)]
        [StringLength(
            10,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationCarPlateStringLength")]
        public string Plate { get; set; }

        [StringLength(
            64,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationCarVendorStringLength")]
        public string Vendor { get; set; }

        [StringLength(
            64,
            ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "ValidationCarModelStringLength")]
        public string Model { get; set; }
    }
}