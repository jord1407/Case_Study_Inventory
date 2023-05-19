using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGeneratorService
{
    public interface IInvoice
    {
        /// <summary>
        /// Generate the invoice details of the respective invoice
        /// </summary>
        /// <param name="invoiceDate"></param>
        public void GenerateInvoiceDetails(DateTime invoiceDate);

        /// <summary>
        /// Implementation with SignalR
        /// </summary>
        public void NotifyClients();
    }
}
