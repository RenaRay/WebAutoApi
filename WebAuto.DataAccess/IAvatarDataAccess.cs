using System.Threading.Tasks;

namespace WebAuto.DataAccess
{
    public interface IAvatarDataAccess
    {
        Task<Avatar> FindByIdAsync(int id);

        Task CreateAsync(Avatar avatar);
    }
}
