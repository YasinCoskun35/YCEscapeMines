using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCEscapeMines.Models;
using static YCEscapeMines.Models.Enums;

namespace YCEscapeMines.Utilities
{
    public class FileHelper
    {
        /// <summary>
        /// Parses two number of a point from string.
        /// </summary>
        /// <param name="line">String line from setting file.</param>
        /// <returns>A point object represent X,Y</returns>
        /// <exception cref="InvalidGameSettingException">If any error occurs while parsing string to int pair.Throws InvalidGameSettingException</exception>
        public static Point ReadPoint(string line)
        {
            try
            {
                var points = line.Split(' ').Select(num => Convert.ToInt32(num)).ToList();
                return new Point(points[1], points[0]);
            }
            catch
            {
                throw new InvalidGameSettingException("Your game setting file is invalid.Please check file edit as required.");
            }
        }
        public static List<Point> ReadPointSerie(string line)
        {
            try
            {
                var points = line.Split(' ').ToList();
                var coordinates = new List<Point>();
                foreach (var point in points)
                {
                    coordinates.Add(new Point(point.Split(',').Select(a => Convert.ToInt32(a)).Reverse().ToArray()));
                }
                return coordinates;
            }
            catch
            {
                throw new InvalidGameSettingException("Your game setting file is invalid.Please check file edit as required.");
            }
        }
        public static void ReadStartPosition(ref GameSetting setting, string line)
        {
            var points = line.Split(' ').ToList();
            setting.StartPoint = new TurtleLocation()
            {
                Position = new Point(Convert.ToInt32(points[1]), Convert.ToInt32(points[0])),
                Direction = Enum.Parse<Enums.Directions>(points[2])
            };
            if (!IsStartPositionValid(setting))
            {
                throw new InvalidGameSettingException("Start Position is not valid.Please check and edit as required.");
            }
        }
        public static bool IsStartPositionValid(GameSetting gameSettings)
        {
            return gameSettings.StartPoint.Position.X < gameSettings.BoardSize.X && gameSettings.StartPoint.Position.Y < gameSettings.BoardSize.Y;
        }
        public static List<TurtleAction> ReadActions(string line)
        {
            try
            {
                var actions = line.Replace("\n", "").Replace("\r", " ").Split(' ').Select(action => Enum.Parse<TurtleAction>(action)).ToList();
                return actions;
            }
            catch
            {
                throw new InvalidGameSettingException("Turtle Actions in your setting file is not valid.Please check and edit as required.");
            }
        }
    }
}
