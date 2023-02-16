using System.Collections.Generic;
using System;
using RoomControl.API.Abstractions;
using System.Linq;
using RoomControl.API.Services;

namespace RoomControl.API.Factories
{
    public class DeviceControlFactory : IDeviceControlFactory
    {
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