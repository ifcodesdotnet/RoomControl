#region Imports
using System.Collections.Generic;
using System;
using RoomControl.API.Abstractions;
using System.Linq;
using RoomControl.API.Services;
#endregion

namespace RoomControl.API.Factories
{
    public class DeviceControlFactory : IDeviceControlFactory
    {
        //https://thecodeblogger.com/2022/09/16/net-dependency-injection-one-interface-and-multiple-implementations/

        private readonly IEnumerable<IDeviceControl> _deviceControlServices;

        public DeviceControlFactory(IEnumerable<IDeviceControl> deviceControlServices)
        {
            _deviceControlServices = deviceControlServices;
        }

        public IDeviceControl GetInstance(string token)
        {
            return token switch
            {
                "WemoLightSwitch" => this.GetService(typeof(WemoService)),
                "SonosPlayFive" => this.GetService(typeof(SonosService)),
                
                _ => throw new InvalidOperationException()
            }; ;
        }

        public IDeviceControl GetService(Type type)
        {
            return _deviceControlServices.FirstOrDefault(x => x.GetType() == type)!;
        }
    }
}