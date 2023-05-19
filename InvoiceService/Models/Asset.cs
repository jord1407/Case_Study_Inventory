using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Models
{
    /// <summary>
    /// Asset DTO
    /// </summary>
    public class Asset
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// When an asset has null ValidFrom it means is been valid since the beginning
        /// </summary>
        public DateTime? ValidFrom { get; set; } = null;

        /// <summary>
        /// When an asset has null valid to that means it is active forever
        /// </summary>
        public DateTime? ValidTo { get; set; } = null;
    }
}
