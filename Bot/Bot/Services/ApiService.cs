using Bot.APIs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Services
{
    public class ApiService: IService
    {
        protected readonly IAPI _api;

        public ApiService(IAPI api)
        {
            _api = api;
        }
    }
}
