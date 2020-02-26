namespace NavratHUB.Data.Storage.Relay
{
    public class RelayStorage
    {
        public float Luminosity { get; private set; }
        public void TryParseData(string sensor, float data)
        {
            switch(sensor)
            {
                case "relay-luminosity":
                    this.Luminosity = data;
                    break; 
            }
        }
    }
}