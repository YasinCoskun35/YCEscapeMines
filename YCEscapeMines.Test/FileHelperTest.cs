using System;
using System.IO;
using Xunit;
using YCEscapeMines.Models;
using YCEscapeMines.Services;

namespace YCEscapeMines.Test
{
    public class FileHelperTest
    {

        [Fact]
        public void IsFileExist()
        {
            FileService fileService = new FileService();
            Assert.Throws<FileNotFoundException>(()=>fileService.ReadGameSetting("UnexistingFile.txt"));
        }
        //If any format issue exists in game setting file this exception will be thrown.I didn't implement for each parse proccess.
        [Fact]
        public void IsBoardSizeValid()
        {
            FileService fileService = new FileService();
            Assert.Throws<InvalidGameSettingException>(() => fileService.ReadGameSetting("InvalidBoardSize.txt"));
        }

      
    }
}
