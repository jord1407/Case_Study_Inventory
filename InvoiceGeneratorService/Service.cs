using InvoiceGeneratorService.Models;
using Shared;

namespace InvoiceGeneratorService
{
    public class Service<T> : IService where T : IInvoice, new()
    {
        public static async void Generate(DateTime invoiceDate)
        {
            T invoice = new T();

            if (invoiceDate == DateTime.MinValue)
                throw new ArgumentNullException("Invoice argument cannot be null.");

            invoice.GenerateInvoiceDetails(invoiceDate);

            await invoice.GenerateAsync();

            invoice.NotifyClients();
        }
    }
}