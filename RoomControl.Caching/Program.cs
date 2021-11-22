using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RoomControl.Caching.Factories;
using RoomControl.Caching.Factories.Interfaces;
using RoomControl.Caching.Models;
using RoomControl.Caching.Utilities;
using UPnP;

namespace RoomControl.Caching
{
    class Program : ConsoleAppBase
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
        }

        [Command("sonos")]
        public async Task CacheSonosAddresses()
        {
            IDictionary<string, string> sonosConnections = await new SonosScannerFactory().Create();

            string roomDevices = File.ReadAllText(Constants.DEVICE_LIST_FILE_PATH);

            Root roomDeviceList = JsonConvert.DeserializeObject<Root>(roomDevices);

            foreach (RoomDevice device in roomDeviceList.RoomDevices)
            {
                if (sonosConnections.ContainsKey(device.name))
                {
                    device.ipAddress = sonosConnections[device.name];
                }
            }
            
            roomDevices = JsonConvert.SerializeObject(roomDeviceList);

            File.WriteAllText(Constants.DEVICE_LIST_FILE_PATH, roomDevices);
        }

        [Command("wemo")]
        public async Task CacheWemoAddresses()
        {
            IDictionary<string, string> wemoConnections = await new WemoScannerFactory().Create();

            string roomDevices = File.ReadAllText(Constants.DEVICE_LIST_FILE_PATH);

            Root roomDeviceList = JsonConvert.DeserializeObject<Root>(roomDevices);

            foreach (RoomDevice device in roomDeviceList.RoomDevices)
            {
                if (wemoConnections.ContainsKey(device.name))
                {
                    device.ipAddress = wemoConnections[device.name];
                }
            }

            roomDevices = JsonConvert.SerializeObject(roomDeviceList);

            File.WriteAllText(Constants.DEVICE_LIST_FILE_PATH, roomDevices);
        }


        //[Command("test")]
        //public async Task test()
        //{
        //    IDictionary<string, string> connections = new Dictionary<string, string>(); 

        //    IDictionary<string, string> sonosConnections = await new SonosScannerFactory().Create();
        //    IDictionary<string, string> wemoConnections = await new WemoScannerFactory().Create();

        //    //foreach (KeyValuePair<string, string> item in sonosConnections)
        //    //{
        //    //    Console.WriteLine(item.Key);
        //    //    Console.WriteLine(item.Value);
        //    //    Console.WriteLine();
        //    //}

        //    //Console.WriteLine();

        //    //foreach (KeyValuePair<string, string> item in wemoConnections)
        //    //{
        //    //    Console.WriteLine(item.Key);
        //    //    Console.WriteLine(item.Value);
        //    //    Console.WriteLine();
        //    //}



        //    //https://stackoverflow.com/questions/294138/merging-dictionaries-in-c-sharp
        //    //merging  dictionaries with no duplicates
        //    //this is bad code....
        //    //maybe i need to write a method to cache the adderess for each device type...
        //    connections = sonosConnections.Union(wemoConnections).ToDictionary(k => k.Key, v => v.Value);



        //    //Read config file
        //    Root roomDevicesList = JsonConvert.DeserializeObject<Root>(File.ReadAllText(Constants.DEVICE_LIST_FILE_PATH));

        //    Dictionary<string, string> roomDevicesDictionary = roomDevicesList.RoomDevices
        //        .ToDictionary(key => key.name, value => value.ipAddress);


        //    foreach (RoomDevice device in roomDevicesList.RoomDevices)
        //    {
        //        if (connections.ContainsKey(device.name))
        //        {
        //            device.ipAddress = connections[device.name];
        //        }
        //    }







        //    string saveme = JsonConvert.SerializeObject(roomDevicesList);


        //    File.WriteAllText(Constants.DEVICE_LIST_FILE_PATH, saveme);







        //}

    }
}