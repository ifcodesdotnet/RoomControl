namespace RoomControl.API.Abstractions
{
    public interface IDeviceControlFactory
    {
        IDeviceControl GetInstance(string token);
    }
}
