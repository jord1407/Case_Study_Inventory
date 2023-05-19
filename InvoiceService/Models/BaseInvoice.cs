using Shared;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoiceService.Models
{
    public abstract class BaseInvoice
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("date")]
        [Required]
        public DateTime Date { get; set; } = DateTime.MinValue;

        [JsonPropertyName("year")]
        [Required]
        public int Year
        {
            get
            {
                return Date.Year;
            }
        }

        [JsonPropertyName("month")]
        [Required]
        public int Month
        {
            get
            {
                return Date.Month;
            }
        }

        [JsonPropertyName("total")]
        [Required]
        public decimal Total { get; set; } = decimal.MinValue;

        [JsonPropertyName("type")]
        public InvoiceTypes Type { get; } = InvoiceTypes.Service;
    }
}
