using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Extensions
{
    public static class ReflectionExtension
    {
        //este meotod obtendra el valor de lo que sea que se le arroje
        public static string GetPropetyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }

    }
}
