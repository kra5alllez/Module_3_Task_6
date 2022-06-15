using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Configs;
using Module_3_Task_6_Vasylchenko.Services.Interface;

namespace Module_3_Task_6_Vasylchenko.Services
{
    public class BackupService : IBackupService
    {
        private readonly IFileConfigService _fileConfigService;
        private LoggerConfig _loggerConfig;

        public BackupService()
        {
            _fileConfigService = new FileConfigService();
            _loggerConfig = new LoggerConfig();
        }

        public async Task SimpleWriteAsync(int backupNumber)
        {
            _loggerConfig = await _fileConfigService.JsonAsync();
            var text = await SimpleReadAsync($@"{_loggerConfig.DirectoryPath}{_loggerConfig.NameFile}{_loggerConfig.FileExtension}");
            var pathBackup = $"{_loggerConfig.BackUpPath}{DateTime.UtcNow.ToString(_loggerConfig.TimeFormat)}-{backupNumber}{_loggerConfig.FileExtension}";
            await File.WriteAllTextAsync(pathBackup, text);
        }

        public async Task<string> SimpleReadAsync(string readPath)
        {
            var text = await File.ReadAllTextAsync(readPath);

            return text;
        }
    }
}
