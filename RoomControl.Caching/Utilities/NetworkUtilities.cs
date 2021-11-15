using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace RoomControl.Caching.Utilities
{
    internal static class NetworkUtilities
    {
        static string getCurrentComputerIpAddress()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .ToString();
        }

        static string getCurrentConnectedNetworkDefaultGateway(string currentMachineIpAddress)
        {
            IPNetwork ipnetwork = IPNetwork.Parse(currentMachineIpAddress);

            //IPAddress defaultGatewayIpAddress = ipnetwork.FirstUsable;

            //string defaultGatewayIpAddressString = defaultGatewayIpAddress.ToString();


            return ipnetwork.FirstUsable
                .ToString(); 
        }

        static int[] getDefaultGatewayOctets(string defaultGatewayIpAddress)
        {
            string[] octets = defaultGatewayIpAddress.Split('.').ToArray();

            int[] ipAddressOctets = defaultGatewayIpAddress
                .Split('.')
                .Select(Int32.Parse)    
                .ToArray();

            return ipAddressOctets; 

            //return new string[] { }; 
        }
    }
}
