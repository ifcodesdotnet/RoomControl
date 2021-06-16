using ByteDev.Sonos;
using ByteDev.Sonos.Models;
using ByteDev.Sonos.Upnp.Services.Models;
using System;
using WemoNet;

namespace WelcomeHome.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo input;
            Wemo wemo = new Wemo();
            SonosController sonosController = new SonosControllerFactory().Create("10.0.0.5");

            do
            {
                input = System.Console.ReadKey();

                if (Char.ToLower(input.KeyChar) == 'h')
                {
                    wemo.TurnOnWemoPlugAsync("http://10.0.0.9").GetAwaiter().GetResult();
                    sonosController.ClearQueueAsync();
                    sonosController.SetPlayModeAsync(new PlayMode(PlayModeType.RepeatOne));
                    sonosController.AddQueueTrackAsync(trackUri: "x-file-cifs://10.0.0.17/music/Noises/test.mp3", enqueueAsNext: true).Wait();
                    sonosController.SetVolumeAsync(new SonosVolume(70));
                    sonosController.PlayAsync();
                }
                else if (Char.ToLower(input.KeyChar) == 'l')
                {
                    wemo.TurnOffWemoPlugAsync("http://10.0.0.9").GetAwaiter().GetResult();
                    sonosController.ClearQueueAsync();
                }
            }
            while (Char.ToLower(input.KeyChar) != 'e');
        }
    }
}
