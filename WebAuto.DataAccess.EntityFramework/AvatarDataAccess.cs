using System;
using System.Threading.Tasks;

namespace WebAuto.DataAccess.EntityFramework
{
    public class AvatarDataAccess : IAvatarDataAccess
    {
        public Task<Avatar> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Avatar avatar)
        {
            throw new NotImplementedException();
        }
    }
}
