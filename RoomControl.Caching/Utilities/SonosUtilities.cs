using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UPnP;

namespace RoomControl.Caching.Utilities
{
    public class SonosUtilities
    {
        public async Task<Dictionary<string, string>> ScanNetworkForDevicesAsync()
        {
            Dictionary<string, string> deviceAddressDictionary = new Dictionary<string, string>();  

            IEnumerable<Device> sonosDevices = await new Ssdp()
                .SearchUPnPDevicesAsync("ZonePlayer");

            foreach (Device device in sonosDevices)
            {
                string ipAddress = device.FriendlyName
                    .Substring(0, device.FriendlyName.IndexOf(' '));

                string name = device.FriendlyName
                    .Substring(device.FriendlyName.LastIndexOf('-') + 2);

                deviceAddressDictionary.Add(name, ipAddress);  
            }

            return deviceAddressDictionary;
        }
    }
}
