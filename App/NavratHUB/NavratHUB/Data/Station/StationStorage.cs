using System.Collections.Generic;

namespace NavratHUB.Data.Station
{
    public class StationStorage
    {
        public delegate void StationStorageEventHandler(object sender, StationEventArgs e);
        public event StationStorageEventHandler DataReceived;
        public Dictionary<string, List<string>> Storage { get; private set; }

        public StationStorage()
        {
            this.Storage = new Dictionary<string, List<string>>();
        }

        public void Add(string sensor, string data)
        {
            if(!this.Storage.ContainsKey(sensor)) this.Storage.Add(sensor, new List<string>());

            this.Storage[sensor].Add(data);
            this.DataReceived?.Invoke(this, new StationEventArgs(sensor, data));
        }
    }
}