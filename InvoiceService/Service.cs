using AutoMapper;
using InvoiceRepository;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace InvoiceService
{
    public class Service<T> : IService where T : class
    {
        private InvoiceContext _context { get; set; }

        private IMapper _mapper;

        public Service(DbContext context)
        {
            _context = (InvoiceContext)context;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InvoiceProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        public List<T> ReadAll()
        {
            // Get all eligible dates based on year, month and type of the invoices
            var eligibleDates = _context.Invoices
                .GroupBy(x => new { x.Year, x.Month, x.Type })
                .ToList()
                .Select((a, b) => a.Max(x => x.Date));

            // Return all records that matches the eligible dates
            return _context.Invoices
                .Where(x => eligibleDates.Contains(x.Date))
                .Select(_mapper.Map<InvoiceRepository.DataAccess.Invoice, T>)
                .ToList();
        }

        public T Read(int id)
        {
            return _context.Invoices
                .Where(x => x.Id == id)
                .Select(_mapper.Map<InvoiceRepository.DataAccess.Invoice, T>)
                .Single();
        }

        public T Create(T invoice)
        {
            InvoiceRepository.DataAccess.Invoice invoiceDb = _mapper.Map<InvoiceRepository.DataAccess.Invoice>(invoice);

            _context.Invoices.Add(invoiceDb);
            _context.SaveChanges();

            return invoice;
        }
    }
}