using Microsoft.AspNetCore.Mvc;

namespace SnykWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        [HttpPost("download")]
        public IActionResult DownloadReports([FromBody] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return NotFound();
            }

            string dirName = Path.Combine(path, "Reports");

            return Ok();
        }

    }
}
