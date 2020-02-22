
using System.Threading.Tasks;

namespace ROP.Services.Repositories
{
    public interface IUserRepository : IRepository<Models.User, int>
    {
        Task<Models.User> GetByMobileNumber(string mobileNumber);
    }
}