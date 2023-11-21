using Entities.Models;


namespace Repositories.Contracts
{
    public interface IMazeretBilgileriRepository : IRepositoryBase<MazeretBilgileri>
    {
        Task<List<MazeretBilgileri>> GetMazeretBilgi();
    }
}
