using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoticiaMAUI.Models;

namespace NoticiaMAUI.Service
{
   
    public class UsuarioService
    {
        HttpClient client;
        public UsuarioService() 
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://dailybugle.sistemas19.com/")
            };
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            List<Usuario> noticias = null;

            var response = await client.GetAsync("api/Usuario/adminin");

            if (response.IsSuccessStatusCode) //status= 200 ok
            {
                var json = await response.Content.ReadAsStringAsync();
                noticias = JsonConvert.DeserializeObject<List<Usuario>>(json);
            }

            if (noticias != null)
            {
                return noticias;
            }
            else
            {
                return new List<Usuario>();
            }
        }
        public async Task<bool> Insert(Usuario n)
        {


            var json = JsonConvert.SerializeObject(n);
            var response = await client.PostAsync("api/Usuario", new StringContent(json, Encoding.UTF8,
                "application/json"));


            return true;
        }
        public async Task<bool> UpdateUsuario(Usuario n)
        {
            var json = JsonConvert.SerializeObject(n);
            var response = await client.PutAsync("api/Usuario", new StringContent(json, Encoding.UTF8,
                "application/json"));
            return true;
        }
        public async Task<bool> DeleteUsuario(Usuario n)
        {
            var response = await client.DeleteAsync("api/Usuario/" + n.Id);

            return true;
        }

    }
}
