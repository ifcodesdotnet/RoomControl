using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RoomControl.Caching.Utilities
{
    static internal class Constants
    {
        public const string SLASH = @"\"; 
        public const string DEVICE_LIST_FILE = "room-control-caching-config.json";

        public static readonly string DEVICE_LIST_FILE_PATH = Path.GetFullPath(Directory.GetCurrentDirectory() + Constants.SLASH + Constants.DEVICE_LIST_FILE); 
    }
}
