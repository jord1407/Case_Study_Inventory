using InvoiceRepository;
using InvoiceService;
using InvoiceService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private InvoiceContext _dbContext;

        public InvoiceController(InvoiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<InvoiceController>
        [HttpGet]
        public IEnumerable<ServiceInvoice> Get()
        {
            Service<ServiceInvoice> service = new Service<ServiceInvoice>(_dbContext);

            return service.ReadAll();
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        public ServiceInvoice Get(int id)
        {
            Service<ServiceInvoice> service = new Service<ServiceInvoice>(_dbContext);

            return service.Read(id);
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public void Post([FromBody] ServiceInvoice invoice)
        {
            Service<ServiceInvoice> service = new Service<ServiceInvoice>(_dbContext);

            service.Create(invoice);
        }
    }
}
