using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Configs;

namespace Module_3_Task_6_Vasylchenko.Services
{
    public class FileService : IFileService
    {
        private readonly IFileServiceConfig _file;
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private Config _config;
        private string _path;

        public FileService()
        {
            _file = new FileServiceConfig();
        }

        public async Task FileSeve(string text)
        {
            await _semaphoreSlim.WaitAsync();
            _config = await _file.Json();
            var config = _config.Logger;
            _path = $@"{config.DirectoryPath}{DateTime.UtcNow.ToString(config.TimeFormat)}{config.FileExtension}";
            using (StreamWriter streamWriter = new StreamWriter(_path, true, System.Text.Encoding.Default))
            {
                await streamWriter.WriteLineAsync(text);
            }

            _semaphoreSlim.Release();
            string writePath = _path;
        }
    }
}
