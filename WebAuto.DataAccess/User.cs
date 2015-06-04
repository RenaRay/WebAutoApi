using System.Collections.Generic;
using WebAuto.Common;

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

        public string Phone { get; set; }

        public ContactsVisibility ContactsVisibleTo { get; set; }

        public int? AvatarId { get; set; }

        public List<Car> Cars { get; set; }
    }
}
