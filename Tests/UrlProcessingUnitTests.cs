using System;
using System.Collections.Generic;
using ProcessingService.Interfaces;
using Xunit;

namespace Tests
{
    public class UnitTests
    {
        [Theory(DisplayName = "SimpleCheck")]
        [InlineData("", new string[] { })]
        [InlineData(null, null)]
        // [InlineData(2, 3, 5)]
        public void SimpleCheck(string inputText, IEnumerable<string> expectedResult)
        {
            // 1. Arrange
            var cs = new UrlProcessingService();

            // 2. Act 
            var result = cs.GetNormalizedLinks(inputText);

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "ParseValidLinksReturnAllNormalizedLinks")]
        [InlineData("Visit photo hosting sites such as www.flickr.com, 500px.com, www.freeimagehosting.net and "
                    +"https://postimage.io, and upload these two image files, picture.dog.png and picture.cat.jpeg, "
                    +"there.After that share their links at https://www.facebook.com/ and i❤images.ws ",
                    new string[] {"http://www.flickr.com", "http://500px.com", "http://www.freeimagehosting.net",
                    "https://postimage.io","https://www.facebook.com/","http://i❤images.ws" })]
        public void ParseValidLinksReturnAllNormalizedLinks(string inputText, IEnumerable<string> expectedResult)
        {
            // 1. Arrange
            var cs = new UrlProcessingService();

            // 2. Act 
            var result = cs.GetNormalizedLinks(inputText);

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "InvalidLinkParseRight")]
        [InlineData("http://??", new string[] {})]
        [InlineData("http:///a", new string[] { })]
        [InlineData("h://test", new string[] { })]
        [InlineData(":// should fail", new string[] { })]
        [InlineData("http://.www.foo.bar./", new string[] { })]
        public void InvalidLinkParseRight(string inputText, IEnumerable<string> expectedResult)
        {
            // 1. Arrange
            var cs = new UrlProcessingService();

            // 2. Act 
            var result = cs.GetNormalizedLinks(inputText);

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
            var cs = new UrlProcessingService();

            // 2. Act 
            var result = cs.GetNormalizedLinks(inputText);

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }
    }

    //ValidLinkReturnNormalized
    //

    //[Theory]
    //[InlineData(1, 0)]
    //public void TestDivideByZero(int x, int y)
    //{
    //    var cs = new CalcService();

    //    Exception ex = Assert
    //     .Throws<DivideByZeroException>(() => cs.UnsafeDivide(x, y));
    //}
}
