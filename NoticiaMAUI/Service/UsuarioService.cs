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

    }
}
