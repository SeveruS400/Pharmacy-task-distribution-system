using Entities.Models;

namespace Repositories.Contracts
{
    public interface INobetDagilimRepository : IRepositoryBase<NobetDagilim>
    {
        NobetDagilim GetEczaneNobetBilgisiById(int id, int turId);
        List<NobetDagilim> GetAllNobetDagilim(int turId);
        int GetMinNobetCount(int turId);
        void Update(int eczaneId, int turId, int nobetCount);
        void CreateList(List<NobetDagilim> entities);
    }
}
