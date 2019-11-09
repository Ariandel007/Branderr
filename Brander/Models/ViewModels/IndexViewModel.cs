using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Game> Game { get; set; }
        public IEnumerable<Category> Category { get; set; }
    }
}
