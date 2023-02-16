using System.Threading.Tasks;

namespace RoomControl.API
{
    public interface IDeviceControl
    {
        Task<bool> On();

        Task<bool> Off(); 
    }
}
