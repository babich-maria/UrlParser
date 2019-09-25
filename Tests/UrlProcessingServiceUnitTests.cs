using System.Collections.Generic;
using ProcessingService.Interfaces;
using Xunit;

namespace Tests
{
    public class UrlProcessingServiceUnitTests
    {
        const string PATTERN = @"[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(|$!:,.;]*\.(com|ws|io|net|한국)((/[-A-Z0-9\u0080-\uFFFF+&@#/%?=~_)(]*)?)*";

        [Theory(DisplayName = "SimpleCheck")]
        [InlineData("", new string[] { })]
        [InlineData(null, null)]
        public void SimpleCheck(string inputText, IEnumerable<string> expectedResult)
        {
            // 1. Arrange
            var ps = new UrlProcessingService();

            // 2. Act 
            var result = ps.GetNormalizedLinks(inputText, PATTERN);

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "ParseTextReturnAllNormalizedLinks")]
        [InlineData("Visit photo hosting sites such as www.flickr.com, 500px.com, www.freeimagehosting.net and "
                    + "https://postimage.io, and upload these two image files, picture.dog.png and picture.cat.jpeg, "
                    + "there.After that share their links at https://www.facebook.com/ and i❤images.ws ",

                    new string[] {"http://www.flickr.com", "http://500px.com", "http://www.freeimagehosting.net",
                        "https://postimage.io","https://www.facebook.com/","http://i❤images.ws" })]
        public void ParseTextReturnAllNormalizedLinks(string inputText, IEnumerable<string> expectedResult)
        {
            // 1. Arrange
            var ps = new UrlProcessingService();

            // 2. Act 
            var result = ps.GetNormalizedLinks(inputText, PATTERN);

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "TestLinkWithDifferentPositionInText")]
        [InlineData("postimage.io", new string[] { "http://postimage.io" })]
        [InlineData("Visit https://postimage.io", new string[] { "https://postimage.io" })]
        [InlineData("https://postimage.io photo ", new string[] { "https://postimage.io" })]
        [InlineData("Visit https://postimage.io photo ", new string[] { "https://postimage.io" })]
        [InlineData("Visit\nhttps://postimage.io photo ", new string[] { "https://postimage.io" })]
        [InlineData("Visit https://postimage.io\n photo ", new string[] { "https://postimage.io" })]
        [InlineData("https://postimage.io, www.flickr.com", new string[] { "https://postimage.io", "http://www.flickr.com" })]
        [InlineData("https://postimage.io\nwww.flickr.com", new string[] { "https://postimage.io", "http://www.flickr.com" })]
        public void TestLinkInDifferentPositionInText(string inputText, IEnumerable<string> expectedResult)
        {
            // 1. Arrange
            var ps = new UrlProcessingService();

            // 2. Act 
            var result = ps.GetNormalizedLinks(inputText, PATTERN);

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "TestLinkNormalizing")]
        [InlineData("postimage.io", new string[] { "http://postimage.io" })]
        [InlineData("https://postimage.io", new string[] { "https://postimage.io" })]
        [InlineData("http://postimage.io", new string[] { "http://postimage.io" })]
        [InlineData("www.postimage.io", new string[] { "http://www.postimage.io" })]
        public void TestLinkNormalizing(string inputText, IEnumerable<string> expectedResult)
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