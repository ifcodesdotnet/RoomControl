using ByteDev.Sonos;
using ByteDev.Sonos.Models;
using ByteDev.Sonos.Upnp.Services.Models;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using WemoNet;

namespace WelcomeHome.Console
{
    class Program : ConsoleAppBase
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
        }

        [Command("on")]
        public void on()
        {
            Wemo wemo = new Wemo();
            SonosController sonosController = new SonosControllerFactory().Create("10.0.0.2");

            wemo.TurnOnWemoPlugAsync("http://10.0.0.9").GetAwaiter().GetResult();
            sonosController.ClearQueueAsync();
            sonosController.SetPlayModeAsync(new PlayMode(PlayModeType.RepeatOne));
            sonosController.AddQueueTrackAsync(trackUri: "x-file-cifs://10.0.0.17/music/Noises/test.mp3", enqueueAsNext: true).Wait();
            sonosController.SetVolumeAsync(new SonosVolume(70));
            sonosController.PlayAsync();
        }

        [Command("off")]
        public void off()
        {
            Wemo wemo = new Wemo();
            SonosController sonosController = new SonosControllerFactory().Create("10.0.0.5");

            wemo.TurnOffWemoPlugAsync("http://10.0.0.9").GetAwaiter().GetResult();
            sonosController.ClearQueueAsync();
        }
    }
}
