using System;
using System.Collections.Generic;
using System.Linq;
using YCEscapeMines.Models;
using YCEscapeMines.Services;

namespace YCEscapeMines.Game
{
    internal class Program
    {
		static void Main(string[] args)
        {
			FileService service = new FileService();
			var gameSettings=service.ReadGameSetting("GameSettings.txt");
			GameService gameService = new GameService(gameSettings);
			gameService.ExecuteGame();
			Console.WriteLine("Please press a key to close console window");
			Console.ReadLine();
		}
		
	}
}
