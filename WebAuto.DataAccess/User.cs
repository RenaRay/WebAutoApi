using System.Collections.Generic;
using WebAuto.Common;

namespace WebAuto.DataAccess
{
    public class User
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ContactsVisibility ContactsVisibleTo { get; set; }

        public string Avatar { get; set; }

        public List<Car> Cars { get; set; }
    }
}
