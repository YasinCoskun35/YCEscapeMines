using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YCEscapeMines.Models.Enums;

namespace YCEscapeMines.Models
{
    public class Simulation
    {
        public Tile[,] Board { get; set; }
        public Result Result { get; set; }
        public TurtleLocation CurrentLocation { get; set; }
    }
}
