#region Imports
using ByteDev.Sonos;
using ByteDev.Sonos.Models;
using ByteDev.Sonos.Upnp.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomControl.API.Abstractions;
using RoomControl.API.Factories;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WemoNet;
#endregion

namespace RoomControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlController : ControllerBase
    {
        #region Dependency Injection
        private readonly IDeviceControlFactory _factory;

        public ControlController(
             IDeviceControlFactory factory
            )
        {
            _factory = factory;
        } 
        #endregion

        [HttpPost("wemo/on")]
        public async Task<IActionResult> WemoOn()
        {
            //https://thecodeblogger.com/2022/09/16/net-dependency-injection-one-interface-and-multiple-implementations/

            IDeviceControl service = _factory.GetInstance("WemoLightSwitch");

            await service.On(); 

            return Ok();
        }

        [HttpPost("wemo/off")]
        public async Task<IActionResult> WemoOff()
        {
            //https://thecodeblogger.com/2022/09/16/net-dependency-injection-one-interface-and-multiple-implementations/

            IDeviceControl service = _factory.GetInstance("WemoLightSwitch");

            await service.Off();

            return Ok();
        }

        [HttpPost("sonos/queue/clear")]
        public async Task<IActionResult> SonosClearQueue()
        {
            //https://thecodeblogger.com/2022/09/16/net-dependency-injection-one-interface-and-multiple-implementations/

            ISonosControl service = (ISonosControl)_factory.GetInstance("SonosPlayFive");

            await service.ClearQueue(); 

            return Ok();
        }

        [HttpPost("sonos/noise")]
        public async Task<IActionResult> SonosNoise()
        {
            //https://thecodeblogger.com/2022/09/16/net-dependency-injection-one-interface-and-multiple-implementations/

            ISonosControl service = (ISonosControl)_factory.GetInstance("SonosPlayFive");

            await service.PlayWhiteNoise(); 

            return Ok();
        }

        #region dead code


        //[HttpPost("test")]
        //public async Task<IActionResult> test()
        //{
        //    await _wemoLightSwitch.Off();

        //    await _wemoLightSwitch.On();

        //    await _sonosPlayFive.Off();

        //    await _sonosPlayFive.On();

        //    return Ok();
        //}


        //[HttpPost("sonos")]
        //public async Task<IActionResult> Sonos()
        //{
        //    SonosController sonosController = sonosControllerFactory
        //        .Create("10.0.0.3");

        //    //await sonosController
        //    //   .ClearQueueAsync();

        //    await sonosController
        //        .SetPlayModeAsync(new PlayMode(PlayModeType.RepeatOne));

        //    //await sonosController
        //    //    .AddQueueTrackAsync(trackUri: "x-file-cifs://10.0.0.17/music/Noises/test.mp3", enqueueAsNext: true);

        //    await sonosController
        //        .SetVolumeAsync(new SonosVolume(100));

        //    await sonosController.PlayAsync();  

        //    return Ok(); 
        //}
        #endregion
    }
}