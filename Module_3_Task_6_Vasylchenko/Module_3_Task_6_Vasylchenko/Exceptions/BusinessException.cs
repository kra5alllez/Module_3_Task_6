using System;
using System.Collections.Generic;
using System.Text;

namespace Module_3_Task_6_Vasylchenko.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}
