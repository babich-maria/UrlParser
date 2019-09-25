using Services.Interfaces;

namespace Services
{
    public class SettingService : ISettingService
    {
        public string ReadPattern(string path)
        {
            string pattern = System.IO.File.ReadAllText(path);

            return pattern;
        }
    }
}