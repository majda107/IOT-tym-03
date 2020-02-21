using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NavratHUB.Data.Station;
using System;

namespace NavratHUB.Hubs
{
    public class StationHub : Hub
    {
        private readonly StationStorage _storage;
        public StationHub(StationStorage storage)
        {
            this._storage = storage;
        }

        public void SendData(string sensor, string data)
        {
            // Console.WriteLine($"~ RECEIVED DATA! | {sensor} | {data}");
            _storage.Add(sensor, data);
        }     
    }
}