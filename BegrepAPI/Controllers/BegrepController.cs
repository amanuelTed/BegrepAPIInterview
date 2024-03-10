using BegrepAPI.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BegrepAPI.Controllers
{
    [Route("api/begrep")]
    [ApiController]
    public class BegrepController : ControllerBase
    {
        private readonly IConceptService _conceptService;
        private readonly ILogger<BegrepController> _logger;

        public BegrepController(IConceptService conceptService, ILogger<BegrepController> logger)
        {
            _conceptService = conceptService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllBegrep([FromQuery] int page = 1)
        {
            try
            {
                _logger.LogInformation("Fetching concepts request recieved");
                var concepts = await _conceptService.GetAllConceptsAsync(page);
                return Ok(concepts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching concepts");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBegrep(string id)
        {
            try
            {
                _logger.LogInformation($"Fetching concept with ID {id}");
                var concept = await _conceptService.GetConceptAsync(id);

                if (concept == null)
                {
                    return NotFound($"Concept with ID {id} not found.");
                }

                return Ok(concept);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching concept with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while fetching concept with ID {id}");
            }
        }
    }
}
