using Microsoft.AspNetCore.Mvc;

namespace practice3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientCodeController : ControllerBase
    {
        private readonly ILogger<PatientCodeController> _logger;

        public PatientCodeController(ILogger<PatientCodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Generate")]
        public IActionResult GeneratePatientCode([FromQuery] string name, [FromQuery] string lastName, [FromQuery] string ci)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(ci))
                    return BadRequest("All parameters are required.");

                string code = $"{name[0]}{lastName[0]}-{ci}";
                _logger.LogInformation($"Generated Patient Code: {code}");

                return Ok(new { PatientCode = code.ToUpper() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating patient code");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
