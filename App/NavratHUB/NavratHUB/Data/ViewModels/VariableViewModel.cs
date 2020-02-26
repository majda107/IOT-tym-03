using Newtonsoft.Json;

namespace NavratHUB.Data.ViewModels
{
    public class VariableViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set;}

        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("color")]
        public string Color { get; set;}

        [JsonProperty("display")]
        public bool Display {get; set;}

        public VariableViewModel()
        {
            this.Name = "";
            this.Desc = "";
            this.Color = "ff0000";
        }
    }
}