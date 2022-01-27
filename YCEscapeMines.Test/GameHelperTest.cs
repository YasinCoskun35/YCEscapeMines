using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YCEscapeMines.Models;
using YCEscapeMines.Models.CustomTypes;
using YCEscapeMines.Services;
using YCEscapeMines.Utilities;

namespace YCEscapeMines.Test
{
    public class GameHelperTest
    {
        [Fact]
        public void CheckIfGameBoardCreationWorksProperly()
        {
            FileService fileService = new FileService();
            var gameSetting = fileService.ReadGameSetting("GameSettings.txt");
            GameService gameSerivce = new GameService(gameSetting);
            var foundTiles=GameHelper.GenerateTiles(gameSetting.BoardSize.X, gameSetting.BoardSize.Y);
            var expectedTiles = new Tile[gameSetting.BoardSize.X, gameSetting.BoardSize.Y];
            for (int i = 0; i < gameSetting.BoardSize.X; i++)
            {
                for (int j = 0; j < gameSetting.BoardSize.Y; j++)
                {
                    var newTile = new Tile();
                    newTile.IsMine = false;
                    newTile.IsExit = false;
                    expectedTiles[i, j] = newTile;
                }
            }
            var exptectesTilesJson=JsonConvert.SerializeObject(expectedTiles);
            var foundTilesJson = JsonConvert.SerializeObject(foundTiles);
            Assert.Equal(exptectesTilesJson, foundTilesJson);
        }
        [Fact]
        public void CheckIfMineCreationThrowsExceptionWHENMultipleMineExist()
        {
            FileService fileService = new FileService();
            var gameSetting = fileService.ReadGameSetting("MultipleMines.txt");
            GameService gameSerivce = new GameService(gameSetting);
            var gameBoard = GameHelper.GenerateTiles(gameSetting.BoardSize.X, gameSetting.BoardSize.Y);
            GameHelper.CreateMinesOnBoard(gameSetting, ref gameBoard);
            Assert.Throws<InvalidGameSettingException>(() => GameHelper.CreateMinesOnBoard(gameSetting, ref gameBoard));
            
        }
    }
}
