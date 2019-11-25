using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Models.ViewModels
{
    public class KeyViewModel
    {

        public IEnumerable<Stock> StockList { get; set; }
        public IEnumerable<Game> GameList { get; set; }
        public Key Key { get; set; }
        

    }
}
