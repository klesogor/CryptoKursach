using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.APIs.DTO
{
    public class Response<T>
    {
        public int? StatusCode { get; set; }

        public T Value { get; set; }
    }
}
