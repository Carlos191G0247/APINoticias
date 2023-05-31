using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoticiaMAUI.Models;

namespace NoticiaMAUI.Service
{
    
    public class NoticiaService
    {
        HttpClient client;
        public NoticiaService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44339/")
            };


        }
        public async Task<List<Noticia>> GetNoticias()
        {
            List<Noticia> noticias = null;

            var response = await client.GetAsync("api/Noticia");

            if (response.IsSuccessStatusCode) //status= 200 ok
            {
                var json = await response.Content.ReadAsStringAsync();
                noticias = JsonConvert.DeserializeObject<List<Noticia>>(json);
            }

            if (noticias != null)
            {
                return noticias;
            }
            else
            {
                return new List<Noticia>();
            }
        }
        public async Task<bool> Insert(Noticia n)
        {


            var json = JsonConvert.SerializeObject(n);
            var response = await client.PostAsync("api/Noticia", new StringContent(json, Encoding.UTF8,
                "application/json"));


            return true;
        }
    }
}
