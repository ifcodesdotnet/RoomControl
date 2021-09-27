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
            var test = new Dictionary<string, string>();  

            IEnumerable<Device> sonosDevices = await new Ssdp().SearchUPnPDevicesAsync("ZonePlayer");

            foreach (Device device in sonosDevices)
            {
                string deviceIpAddress = device.FriendlyName.Substring(0, device.FriendlyName.IndexOf(' '));

                string deviceName = device.FriendlyName.Substring(device.FriendlyName.LastIndexOf('-') + 2); 

                test.Add(deviceName, deviceIpAddress);  
            }

            return test;

        }//end method
    }
}
