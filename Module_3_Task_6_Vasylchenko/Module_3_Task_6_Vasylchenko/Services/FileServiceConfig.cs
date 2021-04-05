using System.IO;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Configs;
using Newtonsoft.Json;

namespace Module_3_Task_6_Vasylchenko.Services
{
    public class FileServiceConfig : IFileServiceConfig
    {
        private const string _pathToJsonFile = "config.json";
        public async Task<LoggerConfig> JsonAsync()
        {
            var fs = File.ReadAllText(_pathToJsonFile);
            var config = JsonConvert.DeserializeObject<Config>(fs);
            return await Task.FromResult(config.Logger);
        }
    }
}
