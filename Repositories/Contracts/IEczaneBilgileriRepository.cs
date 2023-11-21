using Entities.Models;

namespace Repositories.Contracts
{
    public interface IEczaneBilgileriRepository : IRepositoryBase<EczaneBilgileri>
    {
        IQueryable<EczaneBilgileri> GetAllEczane();
        List<EczaneBilgileri> GetAllEczanesTypeOfList();
        List<EczaneBilgileri> GetAllEczaneByIdTypeOfList(int Id);
        int GetYasakliEczaneById(int Id);

    }
}
