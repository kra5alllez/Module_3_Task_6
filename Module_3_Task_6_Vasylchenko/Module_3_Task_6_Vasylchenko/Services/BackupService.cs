using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Configs;

namespace Module_3_Task_6_Vasylchenko.Services
{
    public class BackupService
    {
        private readonly FileConfigService _fileConfigService = new FileConfigService();
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private LoggerConfig _loggerConfig = new LoggerConfig();

        public async Task SimpleWriteAsync(int backupNumber)
        {
            _loggerConfig = await _fileConfigService.JsonAsync();
            var text = await SimpleReadAsync($@"{_loggerConfig.DirectoryPath}{_loggerConfig.NameFile}{_loggerConfig.FileExtension}");
            var pathBackup = $"{_loggerConfig.BackUpPath}{DateTime.UtcNow.ToString(_loggerConfig.TimeFormat)}-{backupNumber}{_loggerConfig.FileExtension}";
            await File.WriteAllTextAsync(pathBackup, text);
        }

        private async Task<string> SimpleReadAsync(string readPath)
        {
            await _semaphoreSlim.WaitAsync();
            var text = await File.ReadAllTextAsync(readPath);
            _semaphoreSlim.Release();
            return text;
        }
    }
}
