using AssetRepository;
using Microsoft.EntityFrameworkCore;

namespace Asset.Tests.RepositoryMock
{
    internal class MockAssetContext : AssetContext
    {
        private readonly DbContextOptions<AssetContext> _options;

        public MockAssetContext(DbContextOptions<AssetContext> options)
            : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use the in-memory SQLite database
            optionsBuilder.UseSqlite("DataSource=:memory:");
        }
    }
}
