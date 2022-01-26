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
    public class GameHelper
    {
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
                    throw new Exception("An error occured while putting mines into game board.Please edit mine coordinates from your game setting file.");
                }
            }
        }
        public static void CreateExitOnBoard(GameSetting gameSettings, ref Tile[,] board)
        {

            if (gameSettings.ExitPoint.X <= gameSettings.BoardSize.X && gameSettings.ExitPoint.Y <= gameSettings.BoardSize.Y && !board[gameSettings.ExitPoint.X, gameSettings.ExitPoint.Y].IsMine)
            {
                board[gameSettings.ExitPoint.X, gameSettings.ExitPoint.Y].IsExit = true;
            }
            else
            {
                throw new Exception("An error occured while putting exit point to game board please edit exit point from your game setting file.");
            }

        }

        public static Directions ChangeDirection(TurtleAction action, TurtleLocation location)
        {
            if (location.Direction == Directions.N)
                return TurtleAction.R == action ? Directions.E : Directions.W;
            else if (location.Direction == Directions.E)
                return TurtleAction.R == action ? Directions.S : Directions.W;
            else if (location.Direction == Directions.S)
                return TurtleAction.R == action ? Directions.W : Directions.E;
            else
                return TurtleAction.R == action ? Directions.N : Directions.S;

        }
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
