#region Imports
using ByteDev.Sonos;
using System.Formats.Asn1;
using System.Threading.Tasks; 
#endregion

namespace RoomControl.API.Services
{
    public class SonosService : IDeviceControl
    {
        private readonly SonosController _controller;

        public SonosService(string address)
        {
            SonosControllerFactory _factory = new SonosControllerFactory();

            _controller = _factory.Create(address); 
        }

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
    }
}
