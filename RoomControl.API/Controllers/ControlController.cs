﻿#region Imports
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
        private readonly IDeviceControl _wemoLightSwitch;
        //private readonly IDeviceControl _wemoPlug;
        private readonly IDeviceControl _sonosPlayFive;

        private readonly IDeviceControlFactory _factory;


        public ControlController(
            IDeviceControl wemoLightSwitch
            //, IDeviceControl wemoPlug
            //, IDeviceControl sonosPlayFive
            , IDeviceControlFactory factory
            )
        {
            _wemoLightSwitch = wemoLightSwitch;
            //_wemoPlug = wemoPlug;
            //_sonosPlayFive = sonosPlayFive;
            _factory = factory;
        }


        //public static Wemo wemoController = new Wemo();
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

        [HttpPost("wemo/on")]
        public async Task<IActionResult> WemoOn()
        {
            IDeviceControl service = _factory.GetInstance("WemoLightSwitch");

            await service.On(); 


            //await _wemoLightSwitch.On();

            return Ok();
        }


        [HttpPost("wemo/off")]
        public async Task<IActionResult> WemoOff()
        {

            IDeviceControl service = _factory.GetInstance("WemoLightSwitch");

            await service.Off();
            //await _wemoLightSwitch.Off();

            return Ok();
        }

        [HttpPost("test")]
        public async Task<IActionResult> test()
        {
            await _wemoLightSwitch.Off();

            await _wemoLightSwitch.On();

            await _sonosPlayFive.Off();

            await _sonosPlayFive.On();

            return Ok();
        }
    }
}
