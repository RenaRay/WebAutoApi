﻿using System.Collections.Generic;

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

        //TODO: убрать из модели
        public string Phone { get; set; }

        public int? AvatarId { get; set; }

        public List<Car> Cars { get; set; }
    }
}
