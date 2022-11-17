using Microsoft.EntityFrameworkCore;
using WpfRegistration.Domain.Models;

namespace WpfRegistration.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        #endregion Constructor

        #region Db Sets

        public DbSet<UserModel> Users { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }

        #endregion Db Sets

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConnection.cs);
            base.OnConfiguring(optionsBuilder);
        }

        #endregion Methods
    }
}