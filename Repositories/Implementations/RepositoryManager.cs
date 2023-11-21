using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        #region Ctor
        private readonly DataContext _context;
        private readonly IEczaneBilgileriRepository _eczaneBilgileriepository;
        private readonly IUserRepository _userRepository;
        private readonly ISehirRepository _sehirRepository;
        private readonly IBolgelerRepository _bolgelerRepository;
        private readonly IResmiTatillerRepository _resmiTatillerRepository;
        private readonly IMazeretBilgileriRepository _mazeretBilgileriRepository;
        private readonly INobetlerRepository _nobetlerRepository;
        private readonly INobetDagilimRepository _nobetDagilimRepository;

        public RepositoryManager(DataContext context, 
            IEczaneBilgileriRepository eczaneBilgileriepository,
            IUserRepository userRepository,
            ISehirRepository sehirRepository,
            IBolgelerRepository bolgelerRepository,
            IResmiTatillerRepository resmiTatillerRepository,
            IMazeretBilgileriRepository mazeretBilgileriRepository,
            INobetlerRepository nobetlerRepository,
            INobetDagilimRepository nobetDagilimRepository)
        {
            _context = context;
            _eczaneBilgileriepository = eczaneBilgileriepository;
            _userRepository = userRepository;
            _sehirRepository = sehirRepository;
            _bolgelerRepository = bolgelerRepository;
            _resmiTatillerRepository = resmiTatillerRepository;
            _mazeretBilgileriRepository = mazeretBilgileriRepository;
            _nobetlerRepository = nobetlerRepository;
            _nobetDagilimRepository = nobetDagilimRepository;
        }
        #endregion

        public IEczaneBilgileriRepository EzcaneBilgileri => _eczaneBilgileriepository;
        public IUserRepository User => _userRepository;

        public ISehirRepository Sehir => _sehirRepository;

        public IBolgelerRepository Bolgeler => _bolgelerRepository;

        public IResmiTatillerRepository ResmiTatiller => _resmiTatillerRepository;

        public IMazeretBilgileriRepository MazeretBilgileri => _mazeretBilgileriRepository;

        public INobetlerRepository Nobetler => _nobetlerRepository;
        public INobetDagilimRepository NobetDagilim => _nobetDagilimRepository;

        public DbContext CreateDbContext()
        {
            return _context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
