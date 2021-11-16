using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RoomControl.Caching.Models;
using RoomControl.Caching.Utilities;
using UPnP;
using WemoNet;

namespace RoomControl.Caching
{
    class Program : ConsoleAppBase
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
        }

        //[Command("test")]
        [Command("temp")]
        public async Task testAsync()
        {
            string fileName = Path.GetFullPath(Directory.GetCurrentDirectory() + Constants.SLASH + Constants.DEVICE_LIST_FILE);

            try
            {
                IDictionary<string, string> roomDevices = new Dictionary<string, string>();


                SonosUtilities sonosUtilities = new SonosUtilities();
                var sonosDevicesAddresses = await sonosUtilities.ScanNetworkForDevicesAsync(); 

                WemoUtilities wemoUtilities = new WemoUtilities();
                var wemoDeviceAddresses = await wemoUtilities.ScanNetworkForDevicesAsync();




                foreach (var item in sonosDevicesAddresses)
                {
                    if (!roomDevices.ContainsKey(item.Key))
                    {
                        roomDevices.Add(item);  
                    }
                }






                //I NEED TO CLEAN THIS CODE!
                foreach (var item in wemoDeviceAddresses)
                {
                    if (!roomDevices.ContainsKey(item.Key))
                    {
                        if (item.Key.Contains("http"))
                        {
                            var test = item.Key.Replace(@"http://", "");
                            roomDevices.Add(item.Value, test);

                        }
                    }
                }





                //Read config file
                Root roomDevicesList = JsonConvert.DeserializeObject<Root>(File.ReadAllText(fileName));



                var temp = roomDevicesList.RoomDevices
                    .ToDictionary(key => key.name, value => value.ipAddress);






                foreach (var item in roomDevices)
                {
                    if (roomDevices.ContainsKey(item.Key))
                    {
                        temp[item.Key] = item.Value;
                    }
                }





                foreach (var item in roomDevicesList.RoomDevices)
                {
                    if (temp.ContainsKey(item.name))
                    {
                        item.ipAddress = temp[item.name];
                    }

                }


                




                string saveme = JsonConvert.SerializeObject(roomDevicesList); 


                File.WriteAllText(fileName, saveme);


            }
            catch (Exception ex)
            {

                throw;
            }
        }




        [Command("test")]
        public async Task test()
        {
            IDictionary<string, string> sonosConnections = await new SonosScanner().Create();
            IDictionary<string, string> wemoConnections = await new WemoScanner().Create();

            foreach (KeyValuePair<string, string> item in sonosConnections)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
                Console.WriteLine();
            }

            Console.WriteLine();

            foreach (KeyValuePair<string, string> item in wemoConnections)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
                Console.WriteLine();
            }



            Console.ReadLine(); 
        }//end method
    }//end class



    abstract class NetworkScannerFactory
    {
        protected abstract Task<IDictionary<string, string>> CreateDerived();

        public async Task<IDictionary<string, string>> Create()
        {
            return await this.CreateDerived();
        }
    }

    class SonosScanner : NetworkScannerFactory
    {
        protected override async Task<IDictionary<string, string>> CreateDerived()
        {
            IDictionary<string, string> sonosConnectionDictionary = new Dictionary<string, string>();
            
            IEnumerable<Device> sonosDevices = await new Ssdp()
                    .SearchUPnPDevicesAsync("ZonePlayer");

            foreach (Device device in sonosDevices){

                string ipAddress = device.FriendlyName
                    .Substring(0, device.FriendlyName.IndexOf(' '));

                string name = device.FriendlyName
                    .Substring(device.FriendlyName.LastIndexOf('-') + 2);

                sonosConnectionDictionary.Add(name, ipAddress);
            }

            return sonosConnectionDictionary;
        }
    }

    class WemoScanner : NetworkScannerFactory
    {
        private static Wemo wemoController = new Wemo();

        protected override async Task<IDictionary<string, string>> CreateDerived()
        {
            IDictionary<string, string> wemoConnectionDictionary = new Dictionary<string, string>();

            //I should make this into an DefaultGatewayOctetBuilder since this these are sequential steps 
            string ipAddress = NetworkUtilities.getCurrentIpAddress();
            string defaultGateway = NetworkUtilities.getCurrenDefaultGateway(ipAddress);
            int[] defaultGatewayOctets = NetworkUtilities.getDefaultGatewayOctets(defaultGateway);

            wemoConnectionDictionary = await wemoController.GetListOfLocalWemoDevicesAsync(
                Convert.ToInt32(defaultGatewayOctets[0]),
                Convert.ToInt32(defaultGatewayOctets[1]),
                Convert.ToInt32(defaultGatewayOctets[2]));

            return wemoConnectionDictionary;
        }
    }//end class 

}//end namespace