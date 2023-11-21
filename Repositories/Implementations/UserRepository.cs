
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Net.Http;

namespace Repositories.Implementations
{
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public object HttpContext { get; private set; }

        public async Task<Users> GetUser(int id)
        {
            var userWithRole = await _context.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.Id == id);

            return userWithRole;
        }
        public async Task<Users> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }
        public async Task<Users> Login(string email)
        {
            var user = await _context.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) { return null; }
            else { return user; }
           
        }

        public Task<Users> Register(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
