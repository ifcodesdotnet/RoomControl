using UPnP;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoomControl.Caching.Factories.Interfaces;

namespace RoomControl.Caching.Factories
{
    public class SonosScannerFactory : INetworkScannerFactory
    {
        public async Task<IDictionary<string, string>> Create()
        {
            IDictionary<string, string> sonosConnectionDictionary = new Dictionary<string, string>();

            IEnumerable<Device> sonosDevices = await new Ssdp()
                    .SearchUPnPDevicesAsync("ZonePlayer");

            foreach (Device device in sonosDevices)
            {

                string ipAddress = device.FriendlyName
                    .Substring(0, device.FriendlyName.IndexOf(' '));

                string name = device.FriendlyName
                    .Substring(device.FriendlyName.LastIndexOf('-') + 2);

                sonosConnectionDictionary.Add(name, ipAddress);
            }

            return sonosConnectionDictionary;
        }
    }
}
