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
        public void Check_GameBoardCreation_WorksProperly()
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
        public void Check_MineCreation_ThrowsException_WHEN_MultipleMineExist()
        {
            FileService fileService = new FileService();
            var gameSetting = fileService.ReadGameSetting("MultipleMines.txt");
            GameService gameSerivce = new GameService(gameSetting);
            var gameBoard = GameHelper.GenerateTiles(gameSetting.BoardSize.X, gameSetting.BoardSize.Y);
            GameHelper.CreateMinesOnBoard(gameSetting, ref gameBoard);
            Assert.Throws<InvalidGameSettingException>(() => GameHelper.CreateMinesOnBoard(gameSetting, ref gameBoard));
            
        }
        [Fact]
        public void Check_StartPosition_ThrowsException_WHEN_MineAlreadyExist()
        {
            FileService fileService = new FileService();
            var gameSetting = fileService.ReadGameSetting("ExitAmbiguity.txt");
            GameService gameSerivce = new GameService(gameSetting);
            var gameBoard = GameHelper.GenerateTiles(gameSetting.BoardSize.X, gameSetting.BoardSize.Y);
            GameHelper.CreateMinesOnBoard(gameSetting, ref gameBoard);
            Assert.Throws<InvalidGameSettingException>(() => GameHelper.CreateExitOnBoard(gameSetting, ref gameBoard));

        }
        [Fact]
        public void Check_ChangeDirection_WorksProperly()
        {
            var currentLocation = new TurtleLocation() { Direction = Enums.Directions.N };
            var expectedDirection = Enums.Directions.W;
            //Turn turtle direction to left
            var foundDirection=GameHelper.ChangeDirection(Enums.TurtleAction.L, currentLocation);
            Assert.Equal(expectedDirection, foundDirection);
        }
        [Fact]
        public void Check_MovingTurtle_WorksProperly()
        {
            var boardSize = new Point( 4, 5) ;
            var currentLocation = new TurtleLocation() { Direction = Enums.Directions.N,Position=new Point(2,3) };
            var expectedLocation = new TurtleLocation() { Direction = Enums.Directions.N,Position=new Point(1,3) };
            //Move turtle one unit on north direction
            var foundLocation = GameHelper.MoveTurtle(boardSize, currentLocation);
            var expectedLocationJson = JsonConvert.SerializeObject(expectedLocation);
            var foundLocationJson = JsonConvert.SerializeObject(foundLocation);
            Assert.Equal(expectedLocationJson, foundLocationJson);
        }
        public void CheckIf_ResultCheck_WinGame_WHEN_Target_IsExit()
        {
            Tile targetTile=new Tile() { IsExit = true ,IsMine=false};
            var foundResult=GameHelper.CheckMove(targetTile);
            var expectedResult = Enums.Result.Win;
            Assert.Equal(expectedResult, foundResult);
        }
    }
}
