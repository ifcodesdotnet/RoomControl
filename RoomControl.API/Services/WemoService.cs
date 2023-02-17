#region Imports
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using RoomControl.API.Abstractions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WemoNet;
using WemoNet.Communications;
#endregion

namespace RoomControl.API.Services
{
    //singleton per device I want to control
    public class WemoService : IDeviceControl
    {
        private readonly string _address;
        private readonly Wemo _wemo;

        public WemoService(string address)
        {
            _address = address;
            _wemo = new Wemo();
        }

        public async Task<bool> Off()
        {
            return await _wemo.TurnOffWemoPlugAsync(_address);
        }

        public async Task<bool> On()
        {
            return await _wemo.TurnOnWemoPlugAsync(_address);
        }

        public async Task<bool> Toggle()
        {
            return await _wemo.ToggleWemoPlugAsync(_address);
        }
    }
}
