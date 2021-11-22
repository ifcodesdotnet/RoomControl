using System;
using WemoNet;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoomControl.Caching.Utilities;
using RoomControl.Caching.Factories.Interfaces;
using System.Net;
using System.Linq;
using RoomControl.Caching.Models;
using RoomControl.Caching.Builders;
using RoomControl.Caching.Extensions;

namespace RoomControl.Caching.Factories
{
    public class WemoScannerFactory : INetworkScannerFactory
    {
        private static Wemo wemoController = new Wemo();

        public async Task<IDictionary<string, string>> Create()
        {
            IDictionary<string, string> wemoConnectionDictionary = new Dictionary<string, string>();

            NetworkBuilder builder = new NetworkBuilder();

            Network network = builder.getIpAddress()
                             .getDefaultGateway()
                             .getDefaultGatewayOctets()
                             .Build();

            IDictionary<string, string> wemoDevices = await wemoController.GetListOfLocalWemoDevicesAsync(
                Convert.ToInt32(network.DefaultGetwayOctets[0]),
                Convert.ToInt32(network.DefaultGetwayOctets[1]),
                Convert.ToInt32(network.DefaultGetwayOctets[2]));

            foreach (KeyValuePair<string, string> device in wemoDevices)
            {
                if (!wemoConnectionDictionary.ContainsKey(device.Value))
                {
                    string ipRemovedProcotol = device.Key.RemoveProcotol();

                    wemoConnectionDictionary.Add(device.Value, ipRemovedProcotol); 
                }
            }

            return wemoConnectionDictionary;
        }
    }
}