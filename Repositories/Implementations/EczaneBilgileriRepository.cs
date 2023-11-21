using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Implementations
{
    public class EczaneBilgileriRepository : RepositoryBase<EczaneBilgileri>, IEczaneBilgileriRepository
    {

        public EczaneBilgileriRepository(DataContext context) : base(context)
        {
        }
        public IQueryable<EczaneBilgileri> GetAllEczane()
        {
            return _context.EczaneBilgileris.AsQueryable();

        }
        public List<EczaneBilgileri> GetAllEczanesTypeOfList()
        {
            return _context.EczaneBilgileris.ToList();
        }
        public List<EczaneBilgileri> GetAllEczaneByIdTypeOfList(int Id)
        {
            var eczaneBilgileri =  _context.EczaneBilgileris
                .Include(m => m.Id)
                .ToList();
            return eczaneBilgileri;
        }
        public int GetYasakliEczaneById(int Id)
        {
            // EczaneId'ye göre YasakliEczaneId sütunundaki değeri al
            int? yasakliEczaneId = _context.EczaneBilgileris
                .Where(e => e.Id == Id)
                .Select(e => e.YasakliEzcaneId)
                .FirstOrDefault();

            // Eğer yasaklı eczane yoksa null olacaktır
            return yasakliEczaneId ?? 0;
        }

    }
}
