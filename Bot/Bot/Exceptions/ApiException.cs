using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Exceptions
{
    public class ApiException: Exception
    {
        public string RawData { get; set; }
        public ApiException(string rawData) : base()
        {
            RawData = rawData;
        }
    }
}
