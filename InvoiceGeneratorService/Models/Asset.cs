using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoiceGeneratorService.Models
{
    /// <summary>
    /// Asset DTO
    /// </summary>
    public class Asset
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// When an asset has null ValidFrom it means is been valid since the beginning
        /// </summary>
        [JsonPropertyName("validfrom")]
        public DateTime? ValidFrom { get; set; } = null;

        /// <summary>
        /// When an asset has null valid to that means it is active forever
        /// </summary>
        [JsonPropertyName("validto")]
        public DateTime? ValidTo { get; set; } = null;
    }
}
