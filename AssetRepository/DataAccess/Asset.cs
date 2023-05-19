using System.ComponentModel.DataAnnotations;

namespace AssetRepository.DataAccess
{
    /// <summary>
    /// Asset DTO
    /// </summary>
    public class Asset : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// When an asset has null ValidFrom it means is been valid since the beginning
        /// </summary>
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// When an asset has null valid to that means it is active forever
        /// </summary>
        public DateTime? ValidTo { get; set; }
    }
}
