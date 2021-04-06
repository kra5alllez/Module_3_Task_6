using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Configs;
using Module_3_Task_6_Vasylchenko.Models;

namespace Module_3_Task_6_Vasylchenko.Services
{
    public class FileService : IFileService
    {
        private readonly string _path;
        private readonly IFileConfigService _fileConfigService;
        private readonly SemaphoreSlim _semaphoreSlim;
        private LoggerConfig _loggerConfig;

        public FileService()
        {
            _semaphoreSlim = new SemaphoreSlim(1);
            _fileConfigService = new FileConfigService();
            InitAsync().GetAwaiter().GetResult();
            _path = $@"{_loggerConfig.DirectoryPath}{_loggerConfig.NameFile}{_loggerConfig.FileExtension}";
        }

        public async Task FileSeveAsync(string text)
        {
            await _semaphoreSlim.WaitAsync();
            using (StreamWriter streamWriter = new StreamWriter(_path, true, System.Text.Encoding.Default))
            {
                await streamWriter.WriteLineAsync(text);
            }

            _semaphoreSlim.Release();
        }

        private async Task InitAsync()
        {
            _loggerConfig = await _fileConfigService.JsonAsync();
        }
    }
}
