using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyAdminApp.Services
{
    class WebApiService
    {
        private const string MyAdminAccessDataApiUrl = "http://192.168.1.5:9091";
        private static HttpClient clienteWeb = CrearCliente(MyAdminAccessDataApiUrl);

        private static HttpClient CrearCliente(string url)
        {
            HttpClient c = new HttpClient();
            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return c;
        }

        //api/ApiRegistros
        public static async Task<List<T>> ObtenerItems<T>(string controlador) where T : new()
        {
            HttpResponseMessage respuesta = await clienteWeb.GetAsync(controlador);
            if (respuesta.IsSuccessStatusCode)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                List<T> lista = JsonConvert.DeserializeObject<List<T>>(contenido);
                return lista;
            }
            return new List<T>();
        }

        
        public static async Task<bool> AgregarItem<T>(string controlador, T item) where T : new()
        {
            string body = JsonConvert.SerializeObject(item);
            var contenido = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clienteWeb.PostAsync(controlador, contenido);
            return (respuesta.IsSuccessStatusCode);
        }

        public static async Task<bool> ActualizarItem<T>(string controlador, T item, int id) where T : new()
        {
            var ruta = $"{controlador}/{id}";
            string body = JsonConvert.SerializeObject(item);
            var contenido = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clienteWeb.PutAsync(ruta, contenido);
            return (respuesta.IsSuccessStatusCode);
        }

        public static async Task<bool> EliminarItem(string controlador, int id)
        {
            var ruta = $"{controlador}/{id}";
            HttpResponseMessage respuesta = await clienteWeb.DeleteAsync(ruta);
            return (respuesta.IsSuccessStatusCode);
        }
    }
}
