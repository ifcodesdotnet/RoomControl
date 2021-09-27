using ConsoleAppFramework;
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
            //may need this to get device names?
            //string fileName = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\room-control-caching-config.json");



            SonosUtilities sonosUtilities = new SonosUtilities();
            var devicesAndAddress = await sonosUtilities.ScanNetworkForDevicesAsync(); 

            WemoUtilities wemoUtilities = new WemoUtilities();
            await wemoUtilities.ScanNetworkForDevicesAsync();
        }



    }




    


  

    public class Root
    {
        public List<string> WemoDeviceNames { get; set; }
        public List<object> SosnosDeviceNames { get; set; }
    }


}