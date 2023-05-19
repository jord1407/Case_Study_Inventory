using Shared;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace InvoiceGeneratorService.Models
{
    /// <summary>
    /// Service Invoice DTO
    /// </summary>
    public class ServiceInvoice : BaseInvoice<ServiceInvoice>
    {
        [Required]
        public IEnumerable<Asset> Assets { get; set; } = Enumerable.Empty<Asset>();

        public ServiceInvoice() : base(InvoiceTypes.Service)
        {
        }

        public override void GenerateInvoiceDetails(DateTime invoiceDate)
        {
            Date = invoiceDate;

            string uri = $"{AppSettings.AssetAPI}/api/Asset/EligibleDates/{invoiceDate:O}";

            var result = Request.Get(uri).Result;

            if (result != null)
                Assets = JsonSerializer.Deserialize<IEnumerable<Asset>>(result) ?? new List<Asset>();

            Total = Assets.Sum(x => x.Price);
        }
    }
}
