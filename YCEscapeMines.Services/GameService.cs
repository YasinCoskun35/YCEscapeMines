using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCEscapeMines.Models.CustomTypes;
using YCEscapeMines.Utilities;

namespace YCEscapeMines.Services
{
    public sealed class GameService
    {
        private GameSetting gameSettings;
        private Simulation simulation;
        /// <summary>
        /// Creates a GameService object with GameSetting to execute game.
        /// </summary>
        /// <param name="settings"></param>
        public GameService(GameSetting settings)
        {
            this.gameSettings = settings;
            simulation = new Simulation();
            simulation.Result = Enums.Result.Continue;
        }
        /// <summary>
        /// Executes game after creating game simulation with game settings.
        /// </summary>
        public void ExecuteGame()
        {
            CreateGameSimulation();
            StartGameSimulation();
        }
        /// <summary>
        /// Creates game simulation to execute with game settings.
        /// </summary>
        private void CreateGameSimulation()
        {
            simulation.Board = GameHelper.GenerateTiles(gameSettings.BoardSize.X, gameSettings.BoardSize.Y);
            var tempBoard = simulation.Board;
            GameHelper.CreateMinesOnBoard(gameSettings,ref tempBoard);
            GameHelper.CreateExitOnBoard(gameSettings,ref tempBoard);

            simulation.CurrentLocation = gameSettings.StartPoint;
            simulation.Board = tempBoard;
        }
        /// <summary>
        /// Starts game simulation and executes it while game is running.
        /// </summary>
        private void StartGameSimulation()
        {
            Console.WriteLine("Game has started !!");
            do
            {
                ExecuteAction();
            } while (IsGameOn());
        }
        /// <summary>
        /// Checks if game is not reached a result or is there any move to execute.
        /// </summary>
        /// <returns></returns>
        private bool IsGameOn()
        {
            //If there are no remaining moves game should stop
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
        /// <summary>
        /// Executes a action from moves list from game settings and pops the executed action.
        /// </summary>
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
