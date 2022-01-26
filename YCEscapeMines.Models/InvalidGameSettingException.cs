using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCEscapeMines.Models
{
    public class InvalidGameSettingException : Exception
    {
        public InvalidGameSettingException()
        {
        }

        public InvalidGameSettingException(string message)
            : base(message)
        {
        }

        public InvalidGameSettingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
