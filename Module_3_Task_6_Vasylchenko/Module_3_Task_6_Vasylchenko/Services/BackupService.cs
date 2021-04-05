using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Module_3_Task_6_Vasylchenko.Services
{
    public class BackupService
    {
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public async Task SimpleWriteAsync(string pathBackup, string readPath)
        {
            var text = await SimpleReadAsync(readPath);

            await File.WriteAllTextAsync(pathBackup, text);
        }

        private async Task<string> SimpleReadAsync(string readPath)
        {
            await _semaphoreSlim.WaitAsync();
            var text = await File.ReadAllTextAsync(readPath);
            _semaphoreSlim.Release();
            return text;
        }
    }
}
