using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using NavratHUB.Data.ViewModels;
using Newtonsoft.Json;

namespace NavratHUB.Data.Connection
{
    public class IotClient
    {
        private static readonly string KEY = "zQ76RyFye0Gc8yeS";
        private static readonly string ENDPOINT = "https://api.nag-iot.zcu.cz/v2";
        private readonly HttpClient _httpClient;
        public IotClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;

            this._httpClient.DefaultRequestHeaders.Add("X-Api-Key", KEY);
            this._httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            this._httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        // public IotClient()
        // {
        //     this._httpClient = null;
        // }
        public async Task<bool> CreateVariable(VariableViewModel variable) // WONT WORK BECAUSE NAG API :-) !!!
        {
            // var dict = new Dictionary<string, string>();
            // dict.Add("name", name);

            // var content = new FormUrlEncodedContent(dict);
            // content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // content.Headers.Add("X-Api-Key", KEY);
            // content.Headers.Add("Accept", "application/json");

            var content = new StringContent(JsonConvert.SerializeObject(variable));

            var result = await this._httpClient.PostAsync($"{ENDPOINT}/variable/{variable.Name}", content);
            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<VariableViewModel> GetVariable(string name)
        {
            var response = await this._httpClient.GetAsync($"{ENDPOINT}/variable/{name}");
            if(response.StatusCode != HttpStatusCode.OK) return null;

            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VariableViewModel>(data);
        }

        public async Task<bool> SetVariable(VariableViewModel variable)
        {
            var data = await this.GetVariable(variable.Name);
            if(data == null) return false;

            data.Value = variable.Value;

            var content = new StringContent(JsonConvert.SerializeObject(data));
            var response = await this._httpClient.PutAsync($"{ENDPOINT}/variable/{variable.Name}", content);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<IEnumerable<VariableViewModel>> GetVariables()
        {
            var response = await this._httpClient.GetAsync($"{ENDPOINT}/variables");
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<VariableViewModel>>(data);
        }
    }
}