using System.Collections.Generic;

namespace ProcessingService.Services
{
    public interface IUrlProcessingService
    {
        IEnumerable<string> GetNormalizedLinks(string inputText, string pattern);
    }
}
