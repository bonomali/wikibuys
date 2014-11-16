using MongoDB.Driver; 
using seoWebApplication.Models;
using seoWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Service
{    
    public class DeviceService
    {
        private readonly MongoHelper<Device> _device;
        public DeviceService()
        {
            _device = new MongoHelper<Device>();
        }

        public void Create(Device device)
        {
            _device.Collection.Save(device);
        }

        public IList<Device> GetDevices()
        {
            try
            {
                return _device.Collection.FindAll().ToList<Device>();
            }
            catch (MongoConnectionException)
            {
                return new List<Device>();
            }
        }

    }
}