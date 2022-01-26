using System;
using System.IO;
using Xunit;
using YCEscapeMines.Services;

namespace YCEscapeMines.Test
{
    public class UnitTest1
    {
        [Fact]
        public void IsFileExist()
        {
            FileService fileService = new FileService();
            Assert.Throws<FileNotFoundException>(()=>fileService.ReadGameSetting("UnexistingFile.txt"));
        }
    }
}
