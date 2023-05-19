using Shared;

namespace InvoiceRepository.DataAccess
{
    public class Invoice : BaseEntity
    {
        /// <summary>
        /// Date of issuing
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Cycle Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Cycle Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Total Amount
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Invoice type is used to treat the data stored in the byte array
        /// </summary>
        public InvoiceTypes Type { get; set; } = InvoiceTypes.Service;

        /// <summary>
        /// Data included in the invoice (Ex: services)
        /// </summary>
        public byte[]? Data { get; set; } 
    }
}
