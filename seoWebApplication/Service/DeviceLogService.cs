using MongoDB.Driver; 
using seoWebApplication.Models;
using seoWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Service
{    
    public class DeviceLogService
    {
        private readonly MongoHelper<DeviceLog> _deviceLog;
        public DeviceLogService()
        {
            _deviceLog = new MongoHelper<DeviceLog>();
        }

        public void Create(DeviceLog deviceLog)
        {
            _deviceLog.Collection.Save(deviceLog);
        }

        public IList<DeviceLog> GetDeviceLogs()
        {
            try
            {
                return _deviceLog.Collection.FindAll().ToList<DeviceLog>();
            }
            catch (MongoConnectionException)
            {
                return new List<DeviceLog>();
            }
        }

    }
}