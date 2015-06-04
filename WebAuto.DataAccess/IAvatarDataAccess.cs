using System.Threading.Tasks;

namespace WebAuto.DataAccess
{
    public interface IAvatarDataAccess
    {
        Task<Avatar> FindByIdAsync(string id);

        Task CreateAsync(Avatar avatar);
    }
}
