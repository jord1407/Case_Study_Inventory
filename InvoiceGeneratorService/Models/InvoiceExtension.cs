using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvoiceGeneratorService.Models
{
    public static class InvoiceExtension
    {
        /// <summary>
        /// Generic Generate method which can be used all type of invoices including ServiceInvoice
        /// </summary>
        /// <param name="api"></param>
        public static async Task<string> GenerateAsync<T>(this T invoice) where T : IInvoice, new()
        {
            string uri = $"{AppSettings.InvoiceAPI}/api/Invoice";

            string json = JsonSerializer.Serialize(invoice);

            string result = await Request.Post(uri, json);

            return result;
        }
    }
}
