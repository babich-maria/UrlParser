using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ProcessingService.Services;

namespace ProcessingService.Interfaces
{
    public class UrlProcessingService : IUrlProcessingService
    {
        public IEnumerable<string> GetNormalizedLinks(string inputText)
        {
            if (inputText == null)
                return null;

            //var Text = @"Visit photo hosting sites such as www.flickr.com/dfg/ffff/, 500px.com/wtyu, 
            //        www.freeimagehosting.net and https://postimage.io, 
            //            and upload these two image files, picture.dog.png and picture.cat.jpeg, there. http://➡.ws/䨹
            //        After that share their links at https://www.facebook.com/ and i❤images.ws http://foo.com/blah_blah_(wikipedia)
            //        https://www.example.com/foo/?bar=baz&inga=42&quux";

            // get expression from file
            string pattern = System.IO.File.ReadAllText("./RegExPattern.txt");
            MatchCollection matches = Regex.Matches(inputText,
                pattern,
             //  @"[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(|$!:,.;]*\.(com|ws|io|net)((/[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(]*)?)*",
               RegexOptions.IgnoreCase, TimeSpan.FromSeconds(25));

         //   Replace(testString, "[$1]");
            var links = new List<string>();

            // Output the found URLs
            foreach (Match match in matches)
            {
                links.Add(NormalizeLink(match.Value));
            }

            return links.ToArray();
        }

        private string NormalizeLink(string url)
        {
            if (!url.Contains("://"))
            {
                url = "http://" + url;
            }

            return url;
        }
    }
}