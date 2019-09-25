using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ProcessingService.Services;

namespace ProcessingService.Interfaces
{
    public class UrlProcessingService : IUrlProcessingService
    {
        public IEnumerable<string> GetNormalizedLinks(string inputText, string pattern)
        {
            if (inputText == null)
                return null;

            MatchCollection matches = Regex.Matches(inputText, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(25));
     
            var links = new List<string>();
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
                url = $"http://{url}";
            }

            return url;
        }
    }
}