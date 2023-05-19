using InvoiceRepository.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared;

namespace InvoiceRepository
{
    public class InvoiceContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }

        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();

            modelBuilder
                .Entity<Invoice>()
                .Property(d => d.Type)
                .HasConversion(new EnumToStringConverter<InvoiceTypes>());

            base.OnModelCreating(modelBuilder);
        }
    }
}