using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Module_3_Task_6_Vasylchenko.Services
{
    public interface IFileService
    {
        Task FileSeveAsync(string text);
    }
}
