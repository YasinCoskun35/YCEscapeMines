using System;
using YCEscapeMines.Models.CustomTypes;
using static YCEscapeMines.Models.CustomTypes.Enums;
using YCEscapeMines.Models;

namespace YCEscapeMines.Utilities
{
    public class GameHelper
    {
        /// <summary>
        /// Generates board tiles in a two dimensional array 
        /// </summary>
        /// <param name="x">Row number of array</param>
        /// <param name="y">Column number of array</param>
        /// <returns>Two dimensional array of Tile object</returns>
        public static Tile[,] GenerateTiles(int x, int y)
        {
            var tiles = new Tile[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var newTile = new Tile();
                    newTile.IsMine = false;
                    newTile.IsExit = false;
                    tiles[i, j] = newTile;
                }
            }
            return tiles;
        }
        /// <summary>
        /// Creates mine on Game Board 
        /// </summary>
        /// <param name="gameSettings">Game setting object with reference to get mine positions</param>
        /// <param name="board">Game board with reference to put mines on it.</param>
        /// <exception cref="InvalidGameSettingException">Throws InvalidGameSettingException if a mine position is not empty to put mine on it.</exception>
        public static void CreateMinesOnBoard(GameSetting gameSettings, ref Tile[,] board)
        {
            foreach (var mineCoordinate in gameSettings.MineCoordinates)
            {
                if (mineCoordinate.X <= gameSettings.BoardSize.X && mineCoordinate.Y <= gameSettings.BoardSize.Y && !board[mineCoordinate.X, mineCoordinate.Y].IsMine)
                {
                    board[mineCoordinate.X, mineCoordinate.Y].IsMine = true;
                }
                else
                {
                    throw new InvalidGameSettingException("An error occured while putting mines into game board.Please edit mine coordinates from your game setting file.");
                }
            }
        }
        /// <summary>
        /// Creates exit location on board.
        /// </summary>
        /// <param name="gameSettings">Game setting object with reference to get exit position</param>
        /// <param name="board">Game board with reference to put exit point on it.</param>
        /// <exception cref="InvalidGameSettingException">Throws InvalidGameSettingException if exit position is not empty to put exit location on it</exception>
        public static void CreateExitOnBoard(GameSetting gameSettings, ref Tile[,] board)
        {

            if (gameSettings.ExitPoint.X <= gameSettings.BoardSize.X && gameSettings.ExitPoint.Y <= gameSettings.BoardSize.Y && !board[gameSettings.ExitPoint.X, gameSettings.ExitPoint.Y].IsMine)
            {
                board[gameSettings.ExitPoint.X, gameSettings.ExitPoint.Y].IsExit = true;
            }
            else
            {
                throw new InvalidGameSettingException("An error occured while putting exit point to game board please edit exit point from your game setting file.");
            }

        }
        /// <summary>
        /// Changes turtle's direction on current location.
        /// </summary>
        /// <param name="action">TurtleAction enum to set Direction whether it turns to Left or Right.It should be sent as R or L</param>
        /// <param name="location">TurtleLocation object to get current direction of turtle</param>
        /// <returns>Directions enum to set new direction of turtle.</returns>
        public static Directions ChangeDirection(TurtleAction action, TurtleLocation location)
        {
            int currentEnum = (int)location.Direction;
            var nextDirection= TurtleAction.R == action ? (currentEnum + 1) % 4 : (currentEnum + 3)%4;
            return (Directions)nextDirection;
        }
        /// <summary>
        /// Moves turtle to the next position according to it's direction if next move position is within board limits
        /// </summary>
        /// <param name="boardSize">Point object to check board size for turtle's next move</param>
        /// <param name="location">Current location of turtle to move turtle next position</param>
        /// <returns></returns>
        public static TurtleLocation MoveTurtle(Point boardSize, TurtleLocation location)
        {
            if(location.Direction == Directions.N && location.Position.X >= 0)
            {
                location.Position.X -= 1;
            }
            else if (location.Direction == Directions.W && location.Position.Y >= 0)
            {
                location.Position.Y -= 1;
            }
            else if (location.Direction == Directions.S && location.Position.X < boardSize.X-1)
            {
                location.Position.X += 1;
            }
            else if (location.Direction == Directions.E && location.Position.Y < boardSize.Y-1)
            {
                location.Position.Y += 1;
            }
            return location;
        }
        /// <summary>
        /// Checks move if turtle hits a mine or reach exit point.Otherwise game will continue
        /// </summary>
        /// <param name="targetTile">Tile object which turtle just moved into</param>
        /// <returns>Result enum to check game result</returns>
        public static Result CheckMove(Tile targetTile)
        {
            if (targetTile.IsExit)
                return Result.Win;
            else if (targetTile.IsMine)
                return Result.Loss;
            else
                return Result.Continue;
        }
    }
}
