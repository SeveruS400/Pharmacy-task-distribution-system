using Entities.Models;

namespace Repositories.Contracts
{
    public interface INobetlerRepository : IRepositoryBase<Nobetler>
    {
        List<Nobetler> GetAllNobetlerTypeOfList();
        bool GetLastNobetBool(int Id, DateTime referenceDate);
        public DateTime GetLastNobetDate();
        List<Nobetler> GetAllNobetlerWithin7Days();
    }
}
