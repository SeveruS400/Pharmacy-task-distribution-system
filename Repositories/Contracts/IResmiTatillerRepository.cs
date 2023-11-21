using Entities.Models;

namespace Repositories.Contracts
{
    public interface IResmiTatillerRepository : IRepositoryBase<ResmiTatiller>
    {
        List<DateTime> GetAllResmiTatillerTypeOfList();
        List<DateTime> GetResmiTatillerByDate(DateTime baslangic, DateTime bitis);
    }
}
