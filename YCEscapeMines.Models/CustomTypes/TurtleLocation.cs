﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YCEscapeMines.Models.CustomTypes.Enums;

namespace YCEscapeMines.Models.CustomTypes
{
    public class TurtleLocation
    {
        public Point Position { get; set; }
        public Directions Direction { get; set; }
    }
}
