using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Exceptions;
using Module_3_Task_6_Vasylchenko.Services;

namespace Module_3_Task_6_Vasylchenko
{
    public class Starter
    {
        private readonly Random _random;
        private readonly Logger _logger;
        private readonly Actions _action;
        private readonly BackupService _backupService = new BackupService();

        public Starter()
        {
            _random = new Random();
            _logger = Logger.Instance();
            _action = new Actions();
        }

        public void Poo()
        {
            Task.Run(async () =>
              {
                  await ForAsync("StreamOne_");
              });

            Task.Run(async () =>
            {
                await ForAsync("StreamTwo_");
            });
        }

        public async Task BackUp(int number)
        {
            await _backupService.SimpleWriteAsync(number);
        }

        private async Task ForAsync(string o)
        {
            const int minRandom = 1;
            const int maxRandom = 4;
            for (var i = 0; i < 50; i++)
            {
                try
                {
                    Console.WriteLine(o + "  " + i);
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
