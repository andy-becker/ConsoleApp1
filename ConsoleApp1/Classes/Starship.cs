using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class Starship
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Cost_In_Credits { get; set; }
        public string Length { get; set; }
        public string Max_Atmosphering_Speed { get; set; }
        public string Crew { get; set; }
        public string Passengers { get; set; }
        public string Cargo_Capacity { get; set; }
        public string Consumables { get; set; }
        public string Hyperdrive_Rating { get; set; }
        public string MGLT { get; set; }
        public string Starship_Class { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Edited { get; set; }
    }
}
