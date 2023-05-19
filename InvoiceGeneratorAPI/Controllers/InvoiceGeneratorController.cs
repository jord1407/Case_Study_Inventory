using InvoiceGeneratorService;
using InvoiceGeneratorService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceGeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceGeneratorController : ControllerBase
    {
        // POST api/<InvoiceController>
        [HttpPost("GenerateServiceInvoice")]
        public void GenerateServiceInvoice([FromBody] DateTime invoiceDate)
        {
            Service<ServiceInvoice>.Generate(invoiceDate);
        }
    }
}
