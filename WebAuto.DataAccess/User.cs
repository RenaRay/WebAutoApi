using System;
using System.Collections.Generic;

namespace WebAuto.DataAccess
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int? AvatarId { get; set; }

        public List<Car> Cars { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? FirstLicenseDate { get; set; }

        public char MaritalStatus { get; set; }

        public string Occupation { get; set; }

        public DateTime? BirthDate { get; set; }

        public char Gender { get; set; }

        public string HairColor { get; set; }
    }
}
