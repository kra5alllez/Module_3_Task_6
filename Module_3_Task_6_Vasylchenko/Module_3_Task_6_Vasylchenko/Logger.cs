using System;
using System.Threading;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Configs;
using Module_3_Task_6_Vasylchenko.Models;
using Module_3_Task_6_Vasylchenko.Services;

namespace Module_3_Task_6_Vasylchenko
{
    public class Logger
    {
        private static readonly Logger _instance = new Logger();
        private readonly IFileService _fileService;
        private readonly FileConfigService _fileConfigService = new FileConfigService();
        private readonly Starter _starter = new Starter();
        private LoggerConfig _loggerConfig = new LoggerConfig();

        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private int _backupNumber;

        static Logger()
        {
        }

        private Logger()
        {
            _backupNumber = 1;
            _fileService = new FileService();
            InitAsync().GetAwaiter().GetResult();
            BuckUp += _starter.BackUp;
        }

        public event Func<int, Task> BuckUp;

        public static Logger Instance() => _instance;

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

        private async Task InitAsync()
        {
            _loggerConfig = await _fileConfigService.JsonAsync();
        }
    }
}
