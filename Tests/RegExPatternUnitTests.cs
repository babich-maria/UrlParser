using System.Collections.Generic;
using ProcessingService.Interfaces;
using Xunit;

namespace Tests
{
    public class RegExPatternUnitTests
    {
        const string PATTERN = @"[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(|$!:,.;]*\.(com|ws|io|net|한국)((/[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(]*)?)*";

        [Theory(DisplayName = "InvalidLinkParseRight")]
        [InlineData("http://??", new string[] { })]
        [InlineData("http:///a", new string[] { })]
        [InlineData("h://test", new string[] { })]
        [InlineData(":// should fail", new string[] { })]
        [InlineData("http://.www.foo.bar./", new string[] { })]
        public void InvalidLinkParseRight(string inputText, IEnumerable<string> expectedResult)
        {
            // 1. Arrange
            var ps = new UrlProcessingService();

            // 2. Act 
            var result = ps.GetNormalizedLinks(inputText, PATTERN);

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "ValidLinkParseRight")]
        [InlineData("http://foo.ws/mr_nr", new string[] { "http://foo.ws/mr_nr" })]
        [InlineData("http://✪df.ws/453", new string[] { "http://✪df.ws/453" })]
        [InlineData("http://userid@example.com/", new string[] { "http://userid@example.com/" })]
        [InlineData("http://foo.com/?q=Test%20URL-encoded%20stuff", new string[] { "http://foo.com/?q=Test%20URL-encoded%20stuff" })]
        [InlineData("http://한국.한국", new string[] { "http://한국.한국" })]
        public void ValidLinkParseRight(string inputText, IEnumerable<string> expectedResult)
        {
            // 1. Arrange
            var ps = new UrlProcessingService();

            // 2. Act 
            var result = ps.GetNormalizedLinks(inputText, PATTERN);

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }
    }
}
