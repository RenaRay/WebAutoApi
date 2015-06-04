using System.Threading.Tasks;

namespace WebAuto.DataAccess
{
    public interface IMessageDataAccess
    {
        Task CreateAsync(Message message);
    }
}
