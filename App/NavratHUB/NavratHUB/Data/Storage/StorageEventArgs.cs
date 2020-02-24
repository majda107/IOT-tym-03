using System;

namespace NavratHUB.Data.Storage
{
    public class StorageEventArgs: EventArgs
    {
        public string Sensor { get; private set; }
        public string Data { get; private set; }
        public StorageEventArgs(string sensor, string data)
        {
            this.Sensor = sensor;
            this.Data = data;
        }   
    }
}