using Asset.Tests.RepositoryMock;
using AssetRepository;
using AssetService;
using Microsoft.EntityFrameworkCore;

namespace Asset.Tests
{
    public class TestData
    {
        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { new AssetService.Models.Asset() { Name = "S1", Price = 10 } },
            new object[] { new AssetService.Models.Asset() { Name = "S2", Price = 20 } },
            new object[] { new AssetService.Models.Asset() { Name = "S3", Price = 5 } },
        };

        public TestData() { }
    }

    public class ManagementTests : IDisposable
    {
        private DbContextOptions<AssetContext> _options;
        private MockAssetContext _context;

        public ManagementTests()
        {
            _options = new DbContextOptionsBuilder<AssetContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

            _context = new MockAssetContext(_options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
        }

        [MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
        [Theory]
        public void Create_Read_Valid(AssetService.Models.Asset asset)
        {
            Service service = new Service(_context);

            AssetService.Models.Asset result = service.Create(asset);

            var entity = _context.Assets.First(x => x.Id == result.Id);

            Assert.Equal(asset.Name, entity.Name);
            Assert.Equal(asset.Price, entity.Price);
            Assert.Null(asset.ValidFrom);
            Assert.Null(asset.ValidTo);

            _context.Remove(entity);
        }

        [Fact]
        public void List_Count_3()
        {
            List<AssetService.Models.Asset> assets = new List<AssetService.Models.Asset>()
            {
                new AssetService.Models.Asset() { Name = "S1", Price = 10 },
                new AssetService.Models.Asset() { Name = "S2", Price = 20 },
                new AssetService.Models.Asset() { Name = "S3", Price = 5 }
            };

            Service service = new Service(_context);

            assets.ForEach(x => service.Create(x));

            int count = _context.Assets.Count();

            Assert.Equal(3, count);

            _context.Assets.RemoveRange(_context.Assets.ToList());
        }

        [Fact]
        public void Delete_All()
        {
            List<AssetService.Models.Asset> assets = new List<AssetService.Models.Asset>()
            {
                new AssetService.Models.Asset() { Name = "S1", Price = 10 },
                new AssetService.Models.Asset() { Name = "S2", Price = 20 },
                new AssetService.Models.Asset() { Name = "S3", Price = 5 }
            };

            Service service = new Service(_context);

            assets.ForEach(x => x.Id = service.Create(x).Id);

            assets.ForEach(x => service.Delete(x.Id));

            int count = _context.Assets.Count();

            Assert.Equal(0, count);
        }

        [Fact]
        public void Modify_Valid()
        {
            AssetService.Models.Asset asset = new AssetService.Models.Asset() { Name = "S1", Price = 10 };

            Service service = new Service(_context);

            AssetService.Models.Asset result = service.Create(asset);

            asset.Id = result.Id;

            result.Name = "S2";
            result = service.Modify(result);

            Assert.Equal(asset.Id, result.Id);
            Assert.NotEqual(asset.Name, result.Name);
            Assert.Equal("S2", result.Name);
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
        }
    }
}