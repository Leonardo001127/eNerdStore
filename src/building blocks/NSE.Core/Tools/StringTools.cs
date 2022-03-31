using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE.Core.Tools
{
    public static class StringTools
    {
        public static string ApenasNumeros(this string str)
        {
            return new string(str.Where(c => Char.IsDigit(c)).ToArray());
        }

    }

    public static class ConfigurationExtensions
    {
        public static string GetMessageQueueConnection(this IConfiguration configuration, string name)
        {
            return configuration.GetSection("MessageQueueConnection")?[name];
        }
    }
}
