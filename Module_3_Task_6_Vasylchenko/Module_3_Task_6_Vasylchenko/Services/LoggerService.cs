using System;
using System.Threading;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Configs;
using Module_3_Task_6_Vasylchenko.Models;
using Module_3_Task_6_Vasylchenko.Services;
using Module_3_Task_6_Vasylchenko.Services.Interface;

namespace Module_3_Task_6_Vasylchenko
{
    public class LoggerService : ILoggerService
    {
        private static readonly LoggerService _instance = new LoggerService();
        private readonly IFileService _fileService;
        private readonly IFileConfigService _fileConfigService;
        private readonly Starter _starter = new Starter();
        private readonly SemaphoreSlim _semaphoreSlim;
        private LoggerConfig _loggerConfig;
        private int _backupNumber;

        static LoggerService()
        {
        }

        private LoggerService()
        {
            _backupNumber = 1;
            _fileService = new FileService();
            _fileConfigService = new FileConfigService();
            _semaphoreSlim = new SemaphoreSlim(1);
            _loggerConfig = new LoggerConfig();
            InitAsync().GetAwaiter().GetResult();
        }

        public event Func<int, Task> BuckUp;

        public static LoggerService Instance() => _instance;

        public async Task LogInfoAsync(string message)
        {
            await LogEventAsync(TypeLog.Info, message);
        }

        public async Task LogBsnsExceptionsAsync(string message)
        {
            await LogEventAsync(TypeLog.Warning, message);
        }

        public async Task LogExceptionsAsync(string message)
        {
            await LogEventAsync(TypeLog.Error, message);
        }

        public async Task LogEventAsync(TypeLog typeLog, string message)
        {
            await _semaphoreSlim.WaitAsync();
            if (_backupNumber % _loggerConfig.ConfigurableNumber == 0)
            {
                await BuckUp(_backupNumber);
            }

            _backupNumber++;
            var logMessage = $"{DateTime.UtcNow}: {typeLog}: {message}";
            Console.WriteLine(logMessage);
            await _fileService.FileSeveAsync(logMessage);
            _semaphoreSlim.Release();
        }

        public async Task InitAsync()
        {
            _loggerConfig = await _fileConfigService.JsonAsync();
            BuckUp += _starter.BackUpAsync;
        }
    }
}
