using System.Threading.Tasks;

namespace RoomControl.API.Abstractions
{
    public interface IDeviceControl
    {
        Task<bool> On();

        Task<bool> Off();

        Task<bool> Toggle(); 
    }
}
