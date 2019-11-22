using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class Character
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string Hair_Color { get; set; }
        public string Skin_Color { get; set; }
        public string Eye_Color { get; set; }
        public string Birth_Year { get; set; }
        public string Gender { get; set; }
        public string HomeWorld { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Edited { get; set; }
    }
}
