using ByteDev.Sonos;
using ByteDev.Sonos.Models;
using ByteDev.Sonos.Upnp.Services.Models;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using WemoNet;

namespace RoomControl.Console
{
    class Program : ConsoleAppBase
    {
        public static SonosControllerFactory sonosControllerFactory = new SonosControllerFactory();
        public static Wemo wemoController = new Wemo();
        
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
        }

        [Command("on")]
        public async Task onAsync()
        {
            SonosController sonosController =  sonosControllerFactory
                .Create("10.0.0.2"); 
            
            await sonosController
                .ClearQueueAsync();

            await sonosController
                .SetPlayModeAsync(new PlayMode(PlayModeType.RepeatOne));

            await sonosController
                .AddQueueTrackAsync(trackUri: "x-file-cifs://10.0.0.17/music/Noises/test.mp3", enqueueAsNext: true);

            await sonosController
                .SetVolumeAsync(new SonosVolume(100));

            await Task.WhenAll(
                sonosController.PlayAsync(), 
                wemoController.TurnOnWemoPlugAsync("http://10.0.0.9")
                ); 
        }

        [Command("off")]
        public async Task offAsync()
        {
            SonosController sonosController = sonosControllerFactory
               .Create("10.0.0.2");
            
            await Task.WhenAll(
                sonosController.ClearQueueAsync(),
                wemoController.TurnOffWemoPlugAsync("http://10.0.0.9"), 
                wemoController.TurnOffWemoPlugAsync("http://10.0.0.6")
                ); 
        }
    }
}
