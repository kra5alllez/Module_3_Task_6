using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Module_3_Task_6_Vasylchenko.Configs;

namespace Module_3_Task_6_Vasylchenko.Services
{
    public interface IFileServiceConfig
    {
        Task<LoggerConfig> JsonAsync();
    }
}
