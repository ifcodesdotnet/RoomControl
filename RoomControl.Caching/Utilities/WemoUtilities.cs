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
        private static Wemo wemoController = new Wemo();

        public async Task<IDictionary<string, string>> ScanNetworkForDevicesAsync()
        {
            ConcurrentDictionary<string, string> deviceAddressDictionary = new ConcurrentDictionary<string, string>();

            try
            {
                //get current machine ip address
                string currentIpAddress = Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList
                    .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .ToString();

                //get default gateway
                IPNetwork ipnetwork = IPNetwork.Parse(currentIpAddress);

                IPAddress defaultGatewayIpAddress = ipnetwork.FirstUsable;

                string defaultGatewayIpAddressString = defaultGatewayIpAddress.ToString();

                //get default dateway octets
                string[] octets = defaultGatewayIpAddressString.Split('.').ToArray();

                int[] ipAddressOctets = defaultGatewayIpAddressString
                    .Split('.')
                    .Select(Int32.Parse)
                    .ToArray(); 
                
                deviceAddressDictionary = await wemoController.GetListOfLocalWemoDevicesAsync(
                    Convert.ToInt32(octets[0]), 
                    Convert.ToInt32(octets[1]), 
                    Convert.ToInt32(octets[2]));
            }
            catch (NetworkInformationException ex)
            {

            }
            catch (Exception ex)
            {

            }

            return deviceAddressDictionary; 
        }//end method
    }
}