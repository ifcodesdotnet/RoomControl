#region Imports
using ByteDev.Sonos;
using ByteDev.Sonos.Upnp.Services.Models;
using RoomControl.API.Abstractions;
using System.Formats.Asn1;
using System.Threading.Tasks; 
#endregion

namespace RoomControl.API.Services
{
    public class SonosService : ISonosControl
    {
        #region Dependency Injection
        private readonly SonosController _controller;

        public SonosService(string address)
        {
            SonosControllerFactory _factory = new SonosControllerFactory();

            _controller = _factory.Create(address); 
        }
        #endregion

        public async Task<bool> Off()
        {
            try
            {
                //await _controller.ClearQueueAsync();

                await _controller.PauseAsync();

                return true;
            }
            catch 
            {
                return false; 
            }
            
        }

        public async Task<bool> On()
        {
            try
            {
                //await _controller.ClearQueueAsync();

                await _controller.PlayAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ClearQueue() 
        {
            try
            {
                await _controller.ClearQueueAsync();

                return true;
            }
            catch 
            {
                return false; 
            }
        }

        public async Task<bool> PlayWhiteNoise()
        {
            await _controller.ClearQueueAsync();

            await _controller.AddQueueTrackAsync(trackUri: "x-file-cifs://10.0.0.15/test/White Noise White Out Full.mp3", enqueueAsNext: true);

            await _controller.SetPlayModeAsync(new PlayMode(PlayModeType.RepeatOne));

            await _controller.PlayAsync(); 

            return true; 
        }

    }
}