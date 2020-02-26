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
            // this._httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        // public IotClient()
        // {
        //     this._httpClient = null;
        // }
        public async Task<bool> CreateVariable(VariableViewModel variable)
        {
            var content = new StringContent(JsonConvert.SerializeObject(variable), System.Text.Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var test = await content.ReadAsStringAsync();

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
            var content = new StringContent(JsonConvert.SerializeObject(variable), System.Text.Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await this._httpClient.PutAsync($"{ENDPOINT}/variable/{variable.Name}", content);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<IEnumerable<VariableViewModel>> GetVariables()
        {
            var response = await this._httpClient.GetAsync($"{ENDPOINT}/variables");
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<VariableViewModel>>(data);
        }

        public async Task<bool> HandleData(string name, float value)
        {
            var data = await this.GetVariable(name);

            if(data != null)
            {
                data.Value = value;
                return await this.SetVariable(data);
            }
            else 
                return await this.CreateVariable(new VariableViewModel() { Name = name, Value = value });
        }
    }
}