using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomControl.Caching.Factories.Interfaces
{
    public interface INetworkScannerFactory
    {
        //change this to scan maybe ?
        Task<IDictionary<string, string>> Create();
    }
}
