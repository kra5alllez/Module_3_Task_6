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
        private const string Name = "Logger";
        private readonly string _path;
        private IFileServiceConfig _file;
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private LoggerConfig _loggerConfig;
        private BackupService _bachUp = new BackupService();
        private int _backupNumber;

        public FileService()
        {
            _backupNumber = 1;
            _file = new FileServiceConfig();
            InitAsync().GetAwaiter().GetResult();
            BackUp += _bachUp.SimpleWriteAsync;
            _path = $@"{_loggerConfig.DirectoryPath}{Name}{_loggerConfig.FileExtension}";
        }

        public event Func<string, string, Task> BackUp;
        public string PathWrite { get; init; }

        public async Task FileSeveAsync(string text)
        {
            await _semaphoreSlim.WaitAsync();
            using (StreamWriter streamWriter = new StreamWriter(_path, true, System.Text.Encoding.Default))
            {
                await streamWriter.WriteLineAsync(text);
            }

            if (_backupNumber % _loggerConfig.ConfigurableNumber == 0)
            {
                await BackUp($"{_loggerConfig.BackUpPath}{DateTime.UtcNow.ToString(_loggerConfig.TimeFormat)}-{_backupNumber}{_loggerConfig.FileExtension}", _path);
            }

            _backupNumber++;
            _semaphoreSlim.Release();
        }

        private async Task InitAsync()
        {
            _loggerConfig = await _file.JsonAsync();
        }
    }
}
