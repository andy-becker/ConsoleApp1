using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class FilmSearchResponse
    {
        public int Count { get; set; }
        public List<Film> Results { get; set; }
    }
}
