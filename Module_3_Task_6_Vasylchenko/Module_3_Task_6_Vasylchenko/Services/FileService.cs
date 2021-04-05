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
        private IFileServiceConfig _file;
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private LoggerConfig _loggerConfig;
        private Model _model;
        private BackupService _bachUp = new BackupService();
        private int _backupNumber;

        public FileService()
        {
            _backupNumber = 1;
            _file = new FileServiceConfig();
            InitAsync().GetAwaiter().GetResult();
            BackUp += _bachUp.SimpleWriteAsync;
        }

        public event Func<string, string, Task> BackUp;

        public async Task FileSeveAsync(string text)
        {
            await _semaphoreSlim.WaitAsync();
            using (StreamWriter streamWriter = new StreamWriter(_model.PathWrite, true, System.Text.Encoding.Default))
            {
                await streamWriter.WriteLineAsync(text);
            }

            if (_backupNumber % _loggerConfig.ConfigurableNumber == 0)
            {
                await BackUp($"{_loggerConfig.BackUpPath}{DateTime.UtcNow.ToString(_loggerConfig.TimeFormat)}-{_backupNumber}{_loggerConfig.FileExtension}", _model.PathWrite);
            }

            _backupNumber++;
            _semaphoreSlim.Release();
        }

        private async Task InitAsync()
        {
            _loggerConfig = await _file.JsonAsync();
            var s = $@"{_loggerConfig.DirectoryPath}{DateTime.UtcNow.ToString(_loggerConfig.TimeFormat)}{_loggerConfig.FileExtension}";
            _model = new Model { PathWrite = s };
        }
    }
}
