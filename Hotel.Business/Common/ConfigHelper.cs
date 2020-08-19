using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.Common
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration { get; set; }

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public static string BaseUrl
        {
            get
            {
                return _configuration.GetValue<string>("AppSettings:BaseUrl");
            }
        }



    }
}
