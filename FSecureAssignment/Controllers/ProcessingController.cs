using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProcessingService.Services;
using Services.Interfaces;

namespace FSecureAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessingController : ControllerBase
    {
        private readonly IUrlProcessingService _urlProcessingService;
        private readonly ISettingService _settingService;

        public ProcessingController(IUrlProcessingService urlProcessingService, ISettingService settingService)
        {
            _urlProcessingService = urlProcessingService;
            _settingService = settingService;
        }

        // POST api/values
        [HttpPost("GetNormalizedLinks")]
        public IActionResult GetNormalizedLinks([FromBody] string text)
        {
            string pattern;
            IEnumerable<string> resultLinks = null;

            try
            {
                pattern = _settingService.ReadPattern(@"./RegExPattern.txt");
            }
            catch
            {
                return BadRequest(new { message = "Something wrong with RegExPattern.txt" });
            }

            try
            {
                resultLinks = _urlProcessingService.GetNormalizedLinks(text, pattern);
            }
            catch
            {
                return BadRequest(new { message = "Could not parse input text" });
            }

            return Ok(new { resultLinks });
        }
    }
}
