using System;
using System.Collections.Generic;
using System.Text;

namespace RoomControl.Caching.Extensions
{
    public static class IpExtensions
    {
        public static string RemoveProcotol(this string value)
        { 
            return value.Replace(@"http://", "");
        }
    }
}
