using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Models.ViewModels
{
    public class OrderDetailsCart
    {
        public List<ShoppingCart> listCart { get; set; }
        public Order Order{ get; set; }
    }
}
