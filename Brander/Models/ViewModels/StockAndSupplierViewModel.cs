using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Models.ViewModels
{
    public class StockAndSupplierViewModel
    {
        public IEnumerable<Supplier> SupplierList { get; set; }
        public Stock Stock { get; set; }
        public List<string> StockList { get; set; }
        public string StatusMessage { get; set; }

    }
}
