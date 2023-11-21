using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.Implementations
{
    public class NobetlerRepository : RepositoryBase<Nobetler>, INobetlerRepository
    {
        public NobetlerRepository(DataContext context) : base(context)
        {
        }

        public List<Nobetler> GetAllNobetlerTypeOfList()
        {
            var Nobetler = _context.Nobetlers
                                    .Include(n => n.EczaneBilgileri)
                                    .OrderBy(n => n.Tarih)
                                    .ToList();
            return Nobetler;
        }
        public bool GetLastNobetBool(int Id, DateTime referenceDate)
        {
            var lastNobet = _context.Nobetlers
                .Where(n => n.EczaneBilgileriId == Id)
                .OrderByDescending(n => n.Tarih)
                .FirstOrDefault();
            // Eğer lastNobet null ise veya 2 günden fazla bir süre varsa
            if (lastNobet == null || (referenceDate - lastNobet.Tarih).TotalDays > 2)
            {
                return true;
            }

            // 2 günden az bir süre varsa
            return false;
        }
        public DateTime GetLastNobetDate()
        {
            var lastNobet = _context.Nobetlers
                .OrderByDescending(n => n.Tarih)
                .FirstOrDefault();
            if (lastNobet == null )
            {
                return DateTime.Today;
            }
            return lastNobet.Tarih;
        }
        public List<Nobetler> GetAllNobetlerWithin7Days()
        {
            DateTime today = DateTime.Now.Date;
            var Nobetler = _context.Nobetlers
                                    .Include(n => n.EczaneBilgileri)
                                    .Where(n => n.Tarih >= today && n.Tarih <= today.AddDays(6))
                                    .OrderBy(n => n.Tarih)
                                    .ToList();
            return Nobetler;
        }

    }
}
