using System.Collections.Generic;

namespace NavratHUB.Data.Station
{
    public class StationStorage
    {
        public Dictionary<string, List<string>> Storage { get; private set; }

        public void Add(string sensor, string data)
        {
            if(!this.Storage.ContainsKey(sensor)) this.Storage.Add(sensor, new List<string>());

            this.Storage[sensor].Add(data);
        }
    }
}