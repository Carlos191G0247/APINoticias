using NoticiaMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace NoticiaMAUI.Service
{
    public class LoginService
    {
        public readonly AuthService auth;
        public string url = "https://dailybugle.sistemas19.com/";
        HttpClient client;

        public LoginService(AuthService auth)
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            this.auth = auth;
        }

        public async Task<bool> IniciarSesion(Usuario login)
        {
            if (string.IsNullOrWhiteSpace(login.Usuario1) || string.IsNullOrWhiteSpace(login.Contraseña))
            {
                throw new ArgumentException("Escriba el nombre de usuario o contraseña");
            }

            var request = await client.PostAsJsonAsync("api/Login", login);
            if (request.IsSuccessStatusCode)
            {

                //read token
                var token = await request.Content.ReadAsStringAsync();

                auth.WriteToken(token);
                return true;
            }
            else
            {
                var message = await request.Content.ReadAsStringAsync();
                return false;
            }
        }
        public async void Logout()
        {
            auth.RemoveToken();
            await Shell.Current.GoToAsync("//MainView");
        }


    }
}
