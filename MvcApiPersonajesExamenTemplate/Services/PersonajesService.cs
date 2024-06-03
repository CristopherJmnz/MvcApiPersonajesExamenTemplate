using MvcApiPersonajesExamenTemplate.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcApiPersonajesExamenTemplate.Services
{
    public class PersonajesService
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public PersonajesService(KeysModel model)
        {
            this.UrlApi = model.ApiUrl;
            this.header = new MediaTypeWithQualityHeaderValue
                ("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/personajes";
            List<Personaje> data =
                await this.CallApiAsync<List<Personaje>>(request);
            return data;
        }

        public async Task<Personaje>
            FindPersonaje(int id)
        {
            string request = "api/personajes/" + id;
            Personaje data =
                await this.CallApiAsync<Personaje>(request);
            return data;
        }

        public async Task CreatePersonajeAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content =
                    new StringContent(json, this.header);
                HttpResponseMessage response =
                    await client.PostAsync(this.UrlApi + request
                    , content);
            }
        }

        public async Task UpdatePersonajeAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content =
                    new StringContent(json, this.header);
                HttpResponseMessage response =
                    await client.PutAsync(this.UrlApi + request
                    , content);
            }
        }

        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/" + idpersonaje;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.DeleteAsync(this.UrlApi + request);
            }
        }

    }
}
