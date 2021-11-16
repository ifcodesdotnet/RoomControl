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
                var temp = roomDevicesList.RoomDevices.ToDictionary(x => x.name, z => z.ipAddress); 




                //foreach (var item in roomDevices)
                //{
                //    if (item.Key == temp[item.Key])
                //    {
                //        temp[item.Key] = item.Value; 
                //    }
                //}

                //Dictionary<string, string> deviceAddresses = roomDevicesList
                //    .RoomDevice.ToDictionary(key => key.name, value => value.ipAddress);


                //string saveme = JsonConvert.SerializeObject(roomDevicesList); 

                //write string to file
                //File.WriteAllText(fileName, saveme);


            }
            catch (Exception ex)
            {

                throw;
            }





        }
    }

    public class RoomDevice
    {
        public string ipAddress { get; set; }
        public string name { get; set; }
    }

    public class Root
    {
        public List<RoomDevice> RoomDevices { get; set; }
    }
}