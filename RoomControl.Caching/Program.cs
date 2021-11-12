﻿using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RoomControl.Caching.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
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

        [Command("test")]
        public async Task testAsync()
        {
            string fileName = Path.GetFullPath(Directory.GetCurrentDirectory() + Constants.SLASH + Constants.DEVICE_LIST_FILE);

            try
            {
                Root roomDevicesList = JsonConvert.DeserializeObject<Root>(File.ReadAllText(fileName));

            }
            catch (Exception ex)
            {

                throw;
            }




            SonosUtilities sonosUtilities = new SonosUtilities();
            var devicesAndAddress = await sonosUtilities.ScanNetworkForDevicesAsync(); 

            WemoUtilities wemoUtilities = new WemoUtilities();
            await wemoUtilities.ScanNetworkForDevicesAsync();
        }
    }

    public class RoomDevice
    {
        public string ipAddress { get; set; }
        public string name { get; set; }
    }

    public class Root
    {
        public List<RoomDevice> RoomDevice { get; set; }
    }
}