using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCEscapeMines.Models;
using YCEscapeMines.Utilities;

namespace YCEscapeMines.Services
{
    public class FileService
    {
        private static string CONSTANT_FILE_PATH = "../../../../YCEscapeMines.Game/GameSettingsFiles/";

        public GameSetting ReadGameSetting(string fileName)
        {
            GameSetting setting = new GameSetting();
            try
            {
                var path = CONSTANT_FILE_PATH + fileName;
                FileStream fileStream = new FileStream(path, FileMode.Open);
                if (fileStream.Length > 0)
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        setting.BoardSize = FileHelper.ReadPoint(reader.ReadLine());
                        setting.MineCoordinates = FileHelper.ReadPointSerie(reader.ReadLine());
                        setting.ExitPoint = FileHelper.ReadPoint(reader.ReadLine());
                        FileHelper.ReadStartPosition(ref setting, reader.ReadLine());
                        setting.Moves = FileHelper.ReadActions(reader.ReadToEnd());
                    }
                }
                else
                    throw new InvalidGameSettingException("Your game setting file is empty.Please check file edit as required.");
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(DirectoryNotFoundException))
                    throw new DirectoryNotFoundException();
                else if (e.GetType() == typeof(FileNotFoundException))
                    throw new FileNotFoundException();
                else if (e.GetType() == typeof(InvalidGameSettingException))
                    throw new InvalidGameSettingException(e.ToString());
            }
            return setting;

        }
    }
}
