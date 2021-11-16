using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace RoomControl.Caching.Utilities
{
    //can i just do a builder pattern here?
    internal static class NetworkUtilities
    {
        public static string getCurrentIpAddress()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .ToString();
        }

        public static string getCurrenDefaultGateway(string ipAddress)
        {
            //IPNetwork ipnetwork = IPNetwork.Parse(ipAddress);

            //IPAddress defaultGatewayIpAddress = ipnetwork.FirstUsable;

            //string defaultGatewayIpAddressString = defaultGatewayIpAddress.ToString();


            //return ipnetwork.FirstUsable
            //    .ToString();


            return IPNetwork.Parse(ipAddress)
                .FirstUsable.ToString();
        }

        public static int[] getDefaultGatewayOctets(string defaultGateway)
        {
            string[] octets = defaultGateway.Split('.').ToArray();

            int[] ipAddressOctets = defaultGateway
                .Split('.')
                .Select(Int32.Parse)    
                .ToArray();

            return ipAddressOctets; 
        }
    }
}
