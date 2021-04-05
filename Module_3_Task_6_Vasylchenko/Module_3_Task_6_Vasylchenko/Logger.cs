using System;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Models;
using Module_3_Task_6_Vasylchenko.Services;

namespace Module_3_Task_6_Vasylchenko
{
    public class Logger
    {
        private static readonly Logger _instance = new Logger();
        private readonly IFileService _fileService;

        static Logger()
        {
        }

        private Logger()
        {
            _fileService = new FileService();
        }

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
            var logMessage = $"{DateTime.UtcNow}: {typeLog}: {message}";
            Console.WriteLine(logMessage);
            await _fileService.FileSeveAsync(logMessage);
        }
    }
}
