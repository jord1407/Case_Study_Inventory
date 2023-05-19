using AssetRepository.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AssetRepository
{
    public class AssetContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();

            base.OnModelCreating(modelBuilder);
        }
    }
}
