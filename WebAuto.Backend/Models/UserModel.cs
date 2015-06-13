using System;

namespace WebAuto.Backend.Models
{
    public class UserModel
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Avatar { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? FirstLicenseDate { get; set; }

        public char MaritalStatus { get; set; }

        public string Occupation { get; set; }

        public DateTime? BirthDate { get; set; }

        public char Gender { get; set; }

        public string HairColor { get; set; }
    }
}