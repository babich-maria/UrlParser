using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using ProcessingService.Services;

namespace FSecureAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessingController : ControllerBase
    {
        private readonly IUrlProcessingService _urlProcessingService;

        public ProcessingController(IUrlProcessingService urlProcessingService)
        {
            _urlProcessingService = urlProcessingService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var Text = @"Visit photo hosting sites such as www.flickr.com/dfg/ffff/, 500px.com/wtyu, 
www.freeimagehosting.net and https://postimage.io, 
and upload these two image files, picture.dog.png and picture.cat.jpeg, there. http://➡.ws/䨹
After that share their links at https://www.facebook.com/ and i❤images.ws http://foo.com/blah_blah_(wikipedia)
  https://www.example.com/foo/?bar=baz&inga=42&quux";

            // Search the input text for URLs (the regular expression pattern is from the excellent
            // "Regular Expressions Cookbook" by Jan Goyvaerts and Steven Levithan)
            //MatchCollection matches = Regex.Matches(Text,
            //    @"\b((https?|ftp|file)://|(www|ftp)\.)[-A-Z0-9+&@#/%?=~_|$!:,.;]*[A-Z0-9+&@#/%=~_|$]",
            //    RegexOptions.IgnoreCase);
            MatchCollection matches = Regex.Matches(Text,
               @"[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(|$!:,.;]*\.(com|ws|io)((/[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(]*)?)*",
               RegexOptions.IgnoreCase, TimeSpan.FromSeconds(25));

            string testString = Text;
            Regex regex = new Regex(
                @"[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(|$!:,.;]*\.(com|ws|io)((/[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(]*)?)*");
            string cleanString = regex.Replace(testString, "[$1$2]{$3}");
           


            //@^(https ?| ftp)://[^\s/$.?#].[^\s]*$@iS


            ///^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/
            IList<string> str = new List<string>();

            // Output the found URLs
            foreach (Match match in matches)
            {
                str.Add(match.Value);
            }


            return str.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("GetNormalizedLinks")]
        public IEnumerable<string> GetNormalizedLinks([FromBody] string text)
        {
            return _urlProcessingService.GetNormalizedLinks(text);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
