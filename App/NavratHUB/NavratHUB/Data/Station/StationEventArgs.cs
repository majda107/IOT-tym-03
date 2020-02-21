using System;

namespace NavratHUB.Data.Station
{
    public class StationEventArgs: EventArgs
    {
        public string Sensor { get; private set; }
        public string Data { get; private set; }
        public StationEventArgs(string sensor, string data)
        {
            this.Sensor = sensor;
            this.Data = data;
        }   
    }
}