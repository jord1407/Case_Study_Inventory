using Invoice.Tests.RepositoryMock;
using InvoiceRepository;
using InvoiceService;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Tests
{
    public class TestData
    {
        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[]
            {
                new InvoiceService.Models.ServiceInvoice()
                {
                    Date = new DateTime(2021, 7, 5),
                    Total = 35,
                    Assets = new List<InvoiceService.Models.Asset>()
                    {
                        new InvoiceService.Models.Asset() { Name = "S1", Price = 10 },
                        new InvoiceService.Models.Asset() { Name = "S2", Price = 20 },
                        new InvoiceService.Models.Asset() { Name = "S3", Price = 5 },
                    }
                }
            },
        };

        public TestData() { }
    }

    public class ManagementTests : IDisposable
    {
        private DbContextOptions<InvoiceContext> _options;
        private MockInvoiceContext _context;

        public ManagementTests()
        {
            _options = new DbContextOptionsBuilder<InvoiceContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

            _context = new MockInvoiceContext(_options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
        }

        [MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
        [Theory]
        public void Create_Read_Valid(InvoiceService.Models.ServiceInvoice invoice)
        {
            Service<InvoiceService.Models.ServiceInvoice> service = new Service<InvoiceService.Models.ServiceInvoice>(_context);

            InvoiceService.Models.ServiceInvoice result = service.Create(invoice);

            var entity = _context.Invoices.First(x => x.Id == result.Id);

            Assert.Equal(invoice.Date, entity.Date);
            Assert.Equal(invoice.Year, entity.Year);
            Assert.Equal(invoice.Month, entity.Month);
            Assert.Equal(invoice.Total, entity.Total);

            _context.Remove(entity);
        }

        [Fact]
        public void List_ReturnLatest_per_MonthYear()
        {
            List<InvoiceService.Models.ServiceInvoice> invoices = new List<InvoiceService.Models.ServiceInvoice>()
            {
                new InvoiceService.Models.ServiceInvoice()
                {
                    Date = new DateTime(2021, 7, 5),
                    Total = 35,
                    Assets = new List<InvoiceService.Models.Asset>()
                    {
                        new InvoiceService.Models.Asset() { Name = "S1", Price = 10 },
                        new InvoiceService.Models.Asset() { Name = "S2", Price = 20 },
                        new InvoiceService.Models.Asset() { Name = "S3", Price = 5 },
                    }
                },
                new InvoiceService.Models.ServiceInvoice()
                {
                    Date = new DateTime(2021, 7, 6),
                    Total = 30,
                    Assets = new List<InvoiceService.Models.Asset>()
                    {
                        new InvoiceService.Models.Asset() { Name = "S1", Price = 5 },
                        new InvoiceService.Models.Asset() { Name = "S2", Price = 20 },
                        new InvoiceService.Models.Asset() { Name = "S3", Price = 5 },
                    }
                }
            };

            Service<InvoiceService.Models.ServiceInvoice> service = new Service<InvoiceService.Models.ServiceInvoice>(_context);

            invoices.ForEach(x => service.Create(x));

            var list = service.ReadAll();

            Assert.Single(list);
            Assert.Equal(invoices[1].Date, list.First().Date);
            Assert.Equal(invoices[1].Year, list.First().Year);
            Assert.Equal(invoices[1].Month, list.First().Month);
            Assert.Equal(invoices[1].Total, list.First().Total);
            Assert.Equivalent(invoices[1].Assets, list.First().Assets);

            _context.Invoices.RemoveRange(_context.Invoices.ToList());
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
        }
    }
}