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
}
