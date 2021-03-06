﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Extensions.SubExtensions
{
    public static class IEnumerableExtensionSupplier
    {
        public static IEnumerable<SelectListItem> ToSelectedItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropetyValue("CompanyName"),
                       Value = item.GetPropetyValue("Id"),
                       Selected = item.GetPropetyValue("Id").Equals(selectedValue.ToString())
                   };
        }
    }
}
