using System;
using System.Collections.Generic;
using static YCEscapeMines.Models.Enums;

namespace YCEscapeMines.Models
{
    public class GameSetting
    {
        public Point BoardSize { get; set; }
        public List<Point> MineCoordinates { get; set; }
        public Point ExitPoint { get; set; }
        public TurtleLocation StartPoint { get; set; }
        public List<TurtleAction> Moves { get; set; }

    }
}
