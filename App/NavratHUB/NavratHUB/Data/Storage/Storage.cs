using System.Collections.Generic;
using NavratHUB.Data.Storage.Station;

namespace NavratHUB.Data.Storage
{
    public class Storage
    {
        public delegate void StationStorageEventHandler(object sender, StorageEventArgs e);
        public event StationStorageEventHandler DataReceived;
        public Dictionary<string, List<string>> Data { get; private set; }

        public StationStorage Station { get; private set; }

        public Storage()
        {
            this.Data = new Dictionary<string, List<string>>();
            this.Station = new StationStorage();
        }

        public void Add(string sensor, string data)
        {
            if(!this.Data.ContainsKey(sensor)) this.Data.Add(sensor, new List<string>());

            this.Station.TryParseData(sensor, data);

            this.Data[sensor].Add(data);
            this.DataReceived?.Invoke(this, new StorageEventArgs(sensor, data));
        }
    }
}