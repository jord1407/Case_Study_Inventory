using Shared;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoiceService.Models
{
    /// <summary>
    /// Invoice DTO
    /// </summary>
    public class ServiceInvoice : BaseInvoice
    {
        [JsonPropertyName("assets")]
        [Required]
        public IEnumerable<Asset> Assets { get; set; } = Enumerable.Empty<Asset>();
    }
}
