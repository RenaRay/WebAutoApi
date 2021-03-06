﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAuto.DataAccess
{
    public interface IUserDataAccess
    {
        Task<User> FindByLoginAsync(string login);

        Task<User> FindByLoginAndPasswordHashAsync(string login, string passwordHash);

        Task<User> FindByPlateExactAsync(string plate);

        Task CreateAsync(User user);

        Task UpdateAsync(User user);
    }
}
