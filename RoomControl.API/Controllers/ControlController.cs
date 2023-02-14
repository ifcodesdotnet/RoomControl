#region Imports
using ByteDev.Sonos;
using ByteDev.Sonos.Models;
using ByteDev.Sonos.Upnp.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WemoNet;
#endregion

namespace RoomControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlController : ControllerBase
    {
        public static Wemo wemoController = new Wemo();
        public static SonosControllerFactory sonosControllerFactory = new SonosControllerFactory();


        [HttpPost("sonos")]
        public async Task<IActionResult> Sonos()
        {
            SonosController sonosController = sonosControllerFactory
                .Create("10.0.0.3");

            //await sonosController
            //   .ClearQueueAsync();

            await sonosController
                .SetPlayModeAsync(new PlayMode(PlayModeType.RepeatOne));

            //await sonosController
            //    .AddQueueTrackAsync(trackUri: "x-file-cifs://10.0.0.17/music/Noises/test.mp3", enqueueAsNext: true);

            await sonosController
                .SetVolumeAsync(new SonosVolume(100));

            await sonosController.PlayAsync();  

            return Ok(); 
        }

        [HttpPost("wemo")]
        public async Task<IActionResult> Wemo(WemoCommand command)
        {
            if (command == WemoCommand.on)
            {
                await wemoController.TurnOnWemoPlugAsync("http://10.0.0.4"); 
            }
            else
            {
                await wemoController.TurnOffWemoPlugAsync("http://10.0.0.4");
            }

            return Ok(command);
        }
    }
}
