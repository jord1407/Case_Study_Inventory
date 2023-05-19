using AssetRepository;
using AssetService;
using AssetService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private AssetContext _dbContext;

        public AssetController(AssetContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<AssetManagementController>
        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            Service service = new Service(_dbContext);

            return service.ReadAll();
        }

        // GET api/<AssetManagementController>/5
        [HttpGet("{id}")]
        public Asset Get(int id)
        {
            Service service = new Service(_dbContext);

            return service.Read(id);
        }

        // GET api/<AssetManagementController>/5
        [HttpGet("EligibleDates/{date}")]
        public IEnumerable<Asset> GetEligibleDates(DateTime date)
        {
            Service service = new Service(_dbContext);

            return service.GetEligibleAssets(date);
        }

        // POST api/<AssetManagementController>
        [HttpPost]
        public void Post([FromBody] Asset asset)
        {
            Service service = new Service(_dbContext);

            service.Create(asset);
        }

        // PUT api/<AssetManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Asset asset)
        {
            Service service = new Service(_dbContext);

            service.Modify(asset);
        }

        // DELETE api/<AssetManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Service service = new Service(_dbContext);

            service.Delete(id);
        }
    }
}
