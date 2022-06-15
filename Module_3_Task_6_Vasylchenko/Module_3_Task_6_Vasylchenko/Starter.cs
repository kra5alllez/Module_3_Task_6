using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Exceptions;
using Module_3_Task_6_Vasylchenko.Services;
using Module_3_Task_6_Vasylchenko.Services.Interface;

namespace Module_3_Task_6_Vasylchenko
{
    public class Starter
    {
        private readonly Random _random;
        private readonly ILoggerService _logger;
        private readonly ActionsControllers _action;
        private readonly IBackupService _backupService;

        public Starter()
        {
            _random = new Random();
            _logger = LoggerService.Instance();
            _action = new ActionsControllers();
            _backupService = new BackupService();
        }

        public void Run()
        {
            Task.Run(async () =>
              {
                  await ForAsync();
              });

            Task.Run(async () =>
            {
                await ForAsync();
            });
        }

        public async Task BackUpAsync(int number)
        {
            await _backupService.SimpleWriteAsync(number);
        }

        private async Task ForAsync()
        {
            const int minRandom = 1;
            const int maxRandom = 4;
            for (var i = 0; i < 50; i++)
            {
                try
                {
                    var random = _random.Next(minRandom, maxRandom);
                    switch (random)
                    {
                        case 1:
                            await _action.InfoMethod();
                            break;
                        case 2:
                            _action.WarningMethod();
                            break;
                        case 3:
                            _action.ErrorMethod();
                            break;
                    }
                }
                catch (BusinessException ex)
                {
                    await _logger.LogBsnsExceptionsAsync($"Action got this custom Exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    await _logger.LogExceptionsAsync($"Action failed by reason: {ex.Message}");
                }
            }
        }
    }
}
