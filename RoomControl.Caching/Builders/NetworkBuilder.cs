using System;
using System.Linq;
using System.Net;
using RoomControl.Caching.Models;

namespace RoomControl.Caching.Builders
{
    public class NetworkBuilder
    {
        private Network network = new Network();

        public NetworkBuilder getIpAddress()
        {
            network.IpAddress = Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .ToString();

            return this;
        }

        public NetworkBuilder getDefaultGateway()
        {
            network.DefaultGateway = IPNetwork.Parse(network.IpAddress)
                .FirstUsable.ToString();

            return this;
        }

        public NetworkBuilder getDefaultGatewayOctets()
        {
            network.DefaultGetwayOctets = network.DefaultGateway
                .Split('.')
                .Select(Int32.Parse)
                .ToArray();

            return this;
        }

        public Network Build()
        {
            return network;
        }
    }
}
