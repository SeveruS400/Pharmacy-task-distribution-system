
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> Options) : base(Options)
        {
        }
        public DbSet<EczaneBilgileri> EczaneBilgileris { get; set; }
        public DbSet<Bolgeler> Bolgelers { get; set; }
        public DbSet<Sehirler> Sehirlers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<MazeretBilgileri> MazeretBilgileri { get; set; }
        public DbSet<ResmiTatiller> ResmiTatiller { get; set; }
        public DbSet<Nobetler> Nobetlers { get; set; }
        public DbSet<NobetTurleri> NobetTuru { get; set; }
        public DbSet<NobetDagilim> NobetDagilim { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedDataAdmin(modelBuilder);
            SeedDataRoles(modelBuilder);
            SeedDataBolgeler(modelBuilder);
            SeedDataSehirler(modelBuilder);
            SeedDataNobetTurleri(modelBuilder);
        }
        private void SeedDataRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleName = "Admin" },
                new UserRole { Id = 2, RoleName = "Editor" },
                new UserRole { Id = 3, RoleName = "User" },
                new UserRole { Id = 4, RoleName = "Visitor" }
            );
        }

        private void SeedDataAdmin(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, UserName = "Admin" , Password="Admin", Email="admin@admin", UserRoleId=1, IsEmailConfirmed=true }

            );
        }
        private void SeedDataBolgeler(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bolgeler>().HasData(
                new Bolgeler { Id = 1, BolgeName = "Merkez"},
                new Bolgeler { Id = 2, BolgeName = "Alaca"}
            );
        }
        private void SeedDataSehirler(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sehirler>().HasData(
                new Sehirler { Id = 1, Sehir = "Çorm"},
                new Sehirler { Id = 2, Sehir = "Ankara"}
            );
        }
        private void SeedDataNobetTurleri(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NobetTurleri>().HasData(
                new NobetTurleri { Id = 1, NobetTuru = "Resmi Tatil" },
                new NobetTurleri { Id = 2, NobetTuru = "Cumartesi" },
                 new NobetTurleri { Id = 3, NobetTuru = "Pazar" },
                new NobetTurleri { Id = 4, NobetTuru = "Haftaici" }
            );
        }

    }
}


