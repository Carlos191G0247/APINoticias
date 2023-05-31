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
                BaseAddress = new Uri("https://dailybugle.sistemas19.com/")
            };


        }
        public async Task<List<Noticia>> GetSalas()
        {
            List<Noticia> noticias = null;

            var response = await client.GetAsync("api/Noticias");

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
        public async Task<Noticia> Insert(Noticia n)
        {
            Noticia noticias;
            var json = JsonConvert.SerializeObject(n);
            StringContent scontent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/Noticias", scontent); // Actualiza la ruta aquí
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonback = await result.Content.ReadAsStringAsync();
                noticias = JsonConvert.DeserializeObject<Noticia>(jsonback);
                return noticias;
            }
            else
            {
                return new Noticia();
            }
        }
    }
}
