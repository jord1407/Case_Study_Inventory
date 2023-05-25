using InvoiceRepository;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Tests.RepositoryMock
{
    internal class MockInvoiceContext : InvoiceContext
    {
        private readonly DbContextOptions<InvoiceContext> _options;

        public MockInvoiceContext(DbContextOptions<InvoiceContext> options)
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
