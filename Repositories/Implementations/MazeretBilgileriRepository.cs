using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;


namespace Repositories.Implementations
{
    public class MazeretBilgileriRepository : RepositoryBase<MazeretBilgileri>, IMazeretBilgileriRepository
    {
        public MazeretBilgileriRepository(DataContext context) : base(context)
        {
        }
        public async Task<List<MazeretBilgileri>> GetMazeretBilgi()
        {
            var mazeretBilgileri = await _context.MazeretBilgileri
                .Include(m => m.EczaneBilgileri)
                .ToListAsync();

            return mazeretBilgileri;
        }
    }
}
