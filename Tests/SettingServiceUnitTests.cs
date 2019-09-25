using System.IO;
using Services;
using Xunit;

namespace Tests
{
    public class SettingServiceUnitTests
    {
        [Theory(DisplayName = "ValidPathReturnPattern")]
        [InlineData(@"./RegExPattern.txt")]
        public void ValidPathReturnPattern(string path)
        {
            // 1. Arrange
            var service = new SettingService();

            // 2. Act 
            var result = service.ReadPattern(path);

            // 3. Assert 
            Assert.NotNull(result);
        }

        [Theory(DisplayName = "InvalidPathReturnException")]
        [InlineData(@"./filenotexist.txt")]
        public void InvalidPathReturnException(string path)
        {
            // 1. Arrange
            var service = new SettingService();

            // 2. Act , Assert 
            Assert.Throws<FileNotFoundException>(() => service.ReadPattern(path));
        }
    }
}
