namespace NavratHUB.Data.Storage.Station
{
    public class StationStorage
    {
        public float Light { get; private set; }
        public float Voltage { get; private set; }
        public float Humidity { get; private set; }
        public float Temperature { get; private set; }

        public bool TryParseData(string sensor, string data) // UGLY!, USE FACTORY STRUCUTRE INSTEAD...
        {
            float value;
            if(!float.TryParse(data, out value))
                return false;

            switch(sensor)
            {
                case "light":
                    this.Light = value;
                    break;

                case "voltage":
                    this.Voltage = value;
                    break;

                case "humidity":
                    this.Humidity = value;
                    break;

                case "temperature":
                    this.Temperature = value;  
                    break;

                default:
                    return false;
            }

            return true;
        }
    }
}