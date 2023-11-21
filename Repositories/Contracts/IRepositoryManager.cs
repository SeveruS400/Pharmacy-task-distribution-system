using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IEczaneBilgileriRepository EzcaneBilgileri { get; }
        IUserRepository User { get; }
        ISehirRepository Sehir { get; }
        IBolgelerRepository Bolgeler { get; }
        IResmiTatillerRepository ResmiTatiller { get; }
        IMazeretBilgileriRepository MazeretBilgileri { get; }
        INobetlerRepository Nobetler { get; }
        INobetDagilimRepository NobetDagilim { get; }
        void Save();
        DbContext CreateDbContext();
    }
}
