using System;

namespace YCEscapeMines.Models
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public  Point(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Point(int[] points)
        {
            this.X= points[0];
            this.Y = points[1];
        }
    }
 
}
