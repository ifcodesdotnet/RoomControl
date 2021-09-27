using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WemoNet;

namespace RoomControl.Caching.Utilities
{
    public class WemoUtilities
    {
        public static Wemo wemoController = new Wemo();

        public async Task ScanNetworkForDevicesAsync()
        {
            try
            {
                string currentIpAddress = Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList
                    .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .ToString();

                IPNetwork ipnetwork = IPNetwork.Parse(currentIpAddress);

                IPAddress defaultGatewayIpAddress = ipnetwork.FirstUsable;

                string defaultGatewayIpAddressString = defaultGatewayIpAddress.ToString();

                string[] octets = defaultGatewayIpAddressString.Split('.').ToArray();

                int[] ipAddressOctets = defaultGatewayIpAddressString
                    .Split('.')
                    .Select(Int32.Parse)
                    .ToArray(); 


                ConcurrentDictionary<string, string> test = await wemoController.GetListOfLocalWemoDevicesAsync(
                    Convert.ToInt32(octets[0]), 
                    Convert.ToInt32(octets[1]), 
                    Convert.ToInt32(octets[2]));


                foreach (var item in test)
                {
                    Console.WriteLine(item.Key);
                    Console.WriteLine(item.Value);
                }
            }
            catch (NetworkInformationException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }//end method

        /// <summary>
        /// This method will bring back the Default Gateway of the currently connected network.
        /// If a connectivity issue were to occer the following exception will get thrown: 
        /// NetworkInformationException
        /// Which I am handling in the methods that will call this one.
        /// https://www.codegrepper.com/code-examples/csharp/Get+the+Default+gateway+address+c%23
        /// </summary>
        /// <returns></returns>
        //public static IPAddress GetDefaultGateway()
        //{
        //    return NetworkInterface
        //        .GetAllNetworkInterfaces()
        //        .Where(n => n.OperationalStatus == OperationalStatus.Up)
        //        .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        //        .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
        //        .Select(g => g?.Address)
        //        .Where(a => a != null)
        //        // .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
        //        // .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
        //        .FirstOrDefault();
        //}
    }
}

