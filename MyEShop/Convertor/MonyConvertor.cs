using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEShop.Convertor
{
    public static class MonyConvertor
    {
        public static string ToRial(this decimal value)
        {
            return value.ToString("#,00");
        }
    }
}
