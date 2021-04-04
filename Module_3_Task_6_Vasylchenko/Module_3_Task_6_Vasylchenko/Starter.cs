using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Exceptions;

namespace Module_3_Task_6_Vasylchenko
{
    public class Starter
    {
        private readonly Random _random;
        private readonly Logger _logger;
        private readonly Actions _action;
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
                  await For(111111);
              });

            Task.Run(async () =>
            {
                await For(222222);
            });
        }

        private async Task For(int o)
        {
            const int minRandom = 1;
            const int maxRandom = 4;
            for (var i = 0; i < 50; i++)
            {
                try
                {
                    Console.WriteLine(o);
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
                    await _logger.LogBsnsExceptions($"Action got this custom Exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    await _logger.LogExceptions($"Action failed by reason: {ex.Message}");
                }
            }
        }
    }
}
