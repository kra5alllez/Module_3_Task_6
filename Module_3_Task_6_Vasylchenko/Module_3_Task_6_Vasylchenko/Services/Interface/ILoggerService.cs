using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Models;

namespace Module_3_Task_6_Vasylchenko.Services.Interface
{
    public interface ILoggerService
    {
        event Func<int, Task> BuckUp;
        Task LogInfoAsync(string message);
        Task LogBsnsExceptionsAsync(string message);

        Task LogExceptionsAsync(string message);

        Task LogEventAsync(TypeLog typeLog, string message);

        Task InitAsync();
    }
}
