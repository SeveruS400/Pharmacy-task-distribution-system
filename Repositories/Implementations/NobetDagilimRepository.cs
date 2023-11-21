using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.Implementations
{
    public class NobetDagilimRepository : RepositoryBase<NobetDagilim>, INobetDagilimRepository
    {
        public NobetDagilimRepository(DataContext context) : base(context)
        {
        }

        public List<NobetDagilim> GetAllNobetDagilim(int turId)
        {
            return _context.NobetDagilim
                .Where(n => n.NobetTuruId == turId)
                .Include(n => n.NobetTuru)
                .ToList();
        }

        public NobetDagilim GetEczaneNobetBilgisiById(int id, int turId)
        {
            var ND = _context.NobetDagilim
                .Where(x => x.EczaneId == id)
                .Where(x => x.NobetTuruId == turId)
                .FirstOrDefault();
            return ND;
        }
        public int GetMinNobetCount(int turId)
        {
            var ND = _context.NobetDagilim
                .Where(e => e.NobetTuruId == turId)
                .OrderBy(n => n.NobetSayisi)
                .Select(n => n.NobetSayisi)
                .FirstOrDefault();
            return ND;
        }
        public void Update(int eczaneId, int turId, int nobetCount)
        {
            var existingEntity = _context.Set<NobetDagilim>()
                .FirstOrDefault(nd => nd.EczaneId == eczaneId && nd.NobetTuruId == turId);

            if (existingEntity != null)
            {
                existingEntity.NobetSayisi = nobetCount; // nobetCount değerini güncelle
                _context.SaveChanges();
            }
        }

        public void CreateList(List<NobetDagilim> entities)
        {
            _context.Set<NobetDagilim>().AddRange(entities);
             _context.SaveChangesAsync();
        }
    }
}
