using System;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Exceptions;

namespace Module_3_Task_6_Vasylchenko
{
    public class Actions
    {
        private readonly Logger _logger = Logger.Instance();
        public async Task<bool> InfoMethod()
        {
            await _logger.LogInfo($"Start method: {nameof(InfoMethod)}");
            return true;
        }

        public bool WarningMethod()
        {
            throw new BusinessException("Skipped logic in method");
        }

        public bool ErrorMethod()
        {
            throw new Exception("I broke a logic");
        }
    }
}
