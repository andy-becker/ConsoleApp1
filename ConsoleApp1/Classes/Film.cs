using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class Film
    {
        public string Title { get; set; }
        public List<string> Characters { get; set; }
        public List<string> Planets { get; set; }
        public List<string> Starships { get; set; }
    }
}
