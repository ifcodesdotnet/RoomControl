#region Imporsts
using System.Threading.Tasks;
#endregion

namespace RoomControl.API.Abstractions
{
    public interface ISonosControl : IDeviceControl
    {
        Task<bool> Pause();

        Task<bool> ClearQueue();

        Task<bool> PlayWhiteNoise(); 
    }
}