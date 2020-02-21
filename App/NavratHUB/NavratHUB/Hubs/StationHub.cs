using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NavratHUB.Data.Station;

namespace NavratHUB.Hubs
{
    public class StationHub : Hub
    {
        private readonly StationStorage _storage;
        public StationHub(StationStorage storage)
        {
            this._storage = storage;
        }
        public async Task SendData(string sensor, string data)
        {
            _storage.Add(sensor, data);
        }     
    }
}