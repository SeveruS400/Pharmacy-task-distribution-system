using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.Implementations
{
    public class ResmiTatillerRepository : RepositoryBase<ResmiTatiller>, IResmiTatillerRepository
    {
        public ResmiTatillerRepository(DataContext context) : base(context)
        {
        }

        public List<DateTime> GetAllResmiTatillerTypeOfList()
        {
            var resmiTatilDates = _context.ResmiTatiller.Select(rt => rt.Tarih).ToList();

            return resmiTatilDates;
        }
        public List<DateTime> GetResmiTatillerByDate(DateTime baslangic, DateTime bitis)
        {
            var resmiTatilDates = _context.ResmiTatiller.Where(rt => rt.Tarih >= baslangic && rt.Tarih <= bitis).Select(rt => rt.Tarih).ToList();
            return resmiTatilDates;
        }
    }
}
