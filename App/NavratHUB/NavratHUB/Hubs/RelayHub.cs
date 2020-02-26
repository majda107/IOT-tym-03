using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NavratHUB.Data.Storage;
using System;

namespace NavratHUB.Hubs
{
    public class RelayHub : Hub // MAY WORK IN FUTURE?
    {
        private readonly Storage _storage;
        public RelayHub(Storage storage)
        {
            this._storage = storage;
        }

        public void SendData(string sensor, string data)
        {
            _storage.Add(sensor, data);
        }     
    }
}