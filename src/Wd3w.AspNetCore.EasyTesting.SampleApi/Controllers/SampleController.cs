using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wd3w.AspNetCore.EasyTesting.SampleApi.Models;
using Wd3w.AspNetCore.EasyTesting.SampleApi.Services;

namespace Wd3w.AspNetCore.EasyTesting.SampleApi.Controllers
{
    [ApiController]
    [Route("api/sample")]
    public class SampleController : ControllerBase
    {
        private readonly ILogger<SampleController> _logger;
        private readonly ISampleService _service;
        private readonly SampleRepository _repository;

        public SampleController(ILogger<SampleController> logger, ISampleService service, SampleRepository repository)
        {
            _logger = logger;
            _service = service;
            _repository = repository;
        }


        [HttpGet("data")]
        public ActionResult<SampleDataResponse> GetSampleData()
        {
            _logger.LogInformation("Call Get SampleData API!");
            return new SampleDataResponse
            {
                Data = _service.GetSampleDate()
            };
        }

        [HttpGet("sample-data-from-db")]
        public async Task<ActionResult> GetRepositorySamplesAsync()
        {
            return Ok(await _repository.GetSamplesAsync());
        }

        [Authorize]
        [HttpGet("secure")]
        public ActionResult GetSecureProcess()
        {
            return NoContent();
        }
    }
}