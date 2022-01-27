using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCEscapeMines.Models.CustomTypes
{
    public class Enums
    {
        public enum Directions
        {
            N,
            E,
            S,
            W
        }

        public enum Result
        {
            Continue,
            Win,
            Loss
        }
        public enum TurtleAction
        {
            M,
            L,
            R
        }
    }
}
