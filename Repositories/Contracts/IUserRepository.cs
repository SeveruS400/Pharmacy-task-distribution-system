
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IUserRepository: IRepositoryBase<Users>
    {
        Task<Users> GetUser(int Id);
        Task<Users> GetUserByEmail(string email);
        Task<Users> Login(string email);
        Task<Users> Register(Users user);
    }
}
