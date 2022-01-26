using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCEscapeMines.Models;
using YCEscapeMines.Utilities;

namespace YCEscapeMines.Services
{
    public sealed class GameService
    {
        private GameSetting gameSettings;
        private Simulation simulation;
        public GameService(GameSetting settings)
        {
            this.gameSettings = settings;
            simulation = new Simulation();
            simulation.Result = Enums.Result.Continue;
        }
        public void ExecuteGame()
        {
            CreateGameSimulation();
            StartGameSimulation();
        }
        private void CreateGameSimulation()
        {
            simulation.Board = GameHelper.GenerateTiles(gameSettings.BoardSize.X, gameSettings.BoardSize.Y);
            var tempBoard = simulation.Board;
            GameHelper.CreateMinesOnBoard(gameSettings,ref tempBoard);
            GameHelper.CreateExitOnBoard(gameSettings,ref tempBoard);

            simulation.CurrentLocation = gameSettings.StartPoint;
            simulation.Board = tempBoard;
        }
        private void StartGameSimulation()
        {
            Console.WriteLine("Game has started !!");
            do
            {
                ExecuteAction();
            } while (IsGameOn());
            Console.WriteLine("Game check");
        }
        private bool IsGameOn()
        {
            bool isRunning = gameSettings.Moves.Count > 0;
            
            switch (simulation.Result)
            {
                case Enums.Result.Continue:
                    if (isRunning)
                        Console.WriteLine("Still in Danger");
                    else
                        Console.WriteLine("You couldn't reach exit point.Please try again after editing your moves");
                    break;
                case Enums.Result.Win:
                    Console.WriteLine("Success");
                    isRunning = false;
                    break;
                case Enums.Result.Loss:
                    Console.WriteLine("Mine Hit.");
                    isRunning = false;
                    break;
                
            }
            return isRunning; 
        }
        private void ExecuteAction()
        {
            var nextMove=gameSettings.Moves.FirstOrDefault();
            switch (nextMove)
            {
                case Enums.TurtleAction.M:
                    simulation.CurrentLocation= GameHelper.MoveTurtle(gameSettings.BoardSize,simulation.CurrentLocation);
                    break;
                default:
                    simulation.CurrentLocation.Direction= GameHelper.ChangeDirection(nextMove,simulation.CurrentLocation);
                    break;
            }
            simulation.Result = GameHelper.CheckMove(simulation.Board[simulation.CurrentLocation.Position.X, simulation.CurrentLocation.Position.Y]);
            gameSettings.Moves.RemoveAt(0);
        }
    }
}
