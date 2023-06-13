using NoticiaMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NoticiaMAUI.Service;
using Microsoft.Maui.Controls;
using NoticiaMAUI.Views;
using System.Security.Claims;

namespace NoticiaMAUI.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        //comandos para jwt
        public Command IniciarSesionCoomand { get; set; }

        private readonly AuthService auth;

        public string Mensaje { get; set; }


        //otros
        public Command VerInicioSesionView { get; set; }
        public Command VerNoticiaCompletaCommand { get; set; }
        public Command VerEditarNoticiaCommand { get; set; }
        public Command EditarNoticiaCommand { get; set; }
        public Command EliminarNoticiaCommand { get; set; }
        public Command EliminarUsuarioCommand { get; set; }
        public Command VerAgregarNoticiaView { get; set; }
        public Command VerNoticiasReporteroCommand { get; set; }
        public Command VerUsuariosViewCommand { get; set; }
        public ICommand SeleccionarImagenCommand { get; }
        public Command AgregarUsuarioCommand { get; set; }
        public Command CancelarUsuarioCommand { get;set; }
        public Command CancelarNoticiasCommand { get; set; }

        public Command AgregarNotciaCommand { get; set; }
        public Command VerAgregarUsuarioViewCommand { get; set; }

        readonly NoticiaService noticiaserver = new NoticiaService();
        readonly UsuarioService usuarioserver = new UsuarioService();
        readonly LoginService login = new LoginService(new AuthService());



        private string _imagePath;
        public Noticia noticiass { get; set; } = new Noticia();
        public Usuario usuarioss { get; set; } = new Usuario();
        public ObservableCollection<Noticia> NoticiaList { get; set; } = new ObservableCollection<Noticia>();
        public ObservableCollection<Usuario> UsuarioList { get; set; } = new ObservableCollection<Usuario>();

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    Actualizar(nameof(ImagePath));
                }
            }
        }

        public ViewModel()
        {
            
            IniciarSesionCoomand = new Command(IniciarSesion);
            //Para Admin
            VerUsuariosViewCommand = new Command(VerUsuario);
            VerAgregarUsuarioViewCommand = new Command(VerAgregarUsuario);
            AgregarUsuarioCommand = new Command(AgregarUsuario);
            CancelarUsuarioCommand = new Command(CancelarUsuario);
            EliminarUsuarioCommand = new Command<Usuario>(EliminarUsuario);
            
            // Para todos
            VerInicioSesionView = new Command(VerSesion);
            VerNoticiaCompletaCommand = new Command<Noticia>(VerNoticiaCompleta);
            // Para Reporteros
            VerAgregarNoticiaView = new Command(VerAgregarRep);
            AgregarNotciaCommand = new Command(AgregarNoticia);
            VerEditarNoticiaCommand = new Command<Noticia>(VerEditarNoticia);
            VerNoticiasReporteroCommand =new Command(VerNoticiasReportero);
            EditarNoticiaCommand = new Command(EditarNoticia);
            EliminarNoticiaCommand = new Command<Noticia>(EliminarNoticia);
            CancelarNoticiasCommand = new Command(CancelarNoticia);
            // Imagen
            SeleccionarImagenCommand = new Command(async () => await SeleccionarImagen());

            CargarNoticias();
            CargarUsuario();
        }

        private async void IniciarSesion()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                { 
                    usuarioss.Id = 2;
                    usuarioss.Nombre = "asdas";
                    if (!await login.IniciarSesion(usuarioss))
                    {
                        Mensaje = "Nombre de usuario o contraseña incorrectas";
                    }
                    else
                    {
                        var userId = ClaimsPrincipal.Current.FindFirst("Id")?.Value;
                        if (userId == "1")
                        {
                            await Shell.Current.GoToAsync("//VerUsuarios");
                        }
                        else
                        {
                            await Shell.Current.GoToAsync("VerNoticiaReport");
                        }
                    }
                }
                else
                    Mensaje = "No hay conexion a internet";

                Actualizar(nameof(Mensaje));
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                Actualizar(nameof(Mensaje));
            }
        }

        private async void CancelarNoticia(object obj)
        {
            await Shell.Current.GoToAsync("VerNoticiaReport");
        }

        private async void EliminarUsuario(Usuario n)
        {
            
            await usuarioserver.DeleteUsuario(n);
            CargarUsuario();
            
        }

        private async void CancelarUsuario()
        {
            await Shell.Current.GoToAsync("//VerUsuarios");
        }

        private async void AgregarUsuario()
        {                       
            await usuarioserver.Insert(usuarioss);
            await Shell.Current.GoToAsync("//VerUsuarios");
            CargarUsuario();
        }

        private async void VerAgregarUsuario()
        {

            await Shell.Current.GoToAsync("//VerAgregarUsuario");
        }

        private async void VerUsuario()
        {
            await Shell.Current.GoToAsync("//VerUsuarios");
        }

        private async void VerEditarNoticia(Noticia n)
        {
            noticiass = n;
            noticiass.Titulo = n.Titulo;
            noticiass.Autor = n.Autor;
            noticiass.Contenido = n.Contenido;
            noticiass.Imagen = n.Imagen;
            noticiass.Fecha = n.Fecha;


            EditarNoticiaView editar = new EditarNoticiaView
            {
                BindingContext = this,
            };
            await Shell.Current.GoToAsync("//EditarNoticia");
        }
        public async void EditarNoticia()
        {
            noticiass.Imagen = ConvertImageToBase64(ImagePath);
            await noticiaserver.UpdateNotcia(noticiass);
            await Shell.Current.GoToAsync("VerNoticiaReport");
            CargarNoticias();
        }

        public async void EliminarNoticia(Noticia n)
        {           
            await noticiaserver.DeleteNoticia(n);          
            CargarNoticias();
        }
        

        private async void VerNoticiasReportero()
        {
            await Shell.Current.GoToAsync("VerNoticiaReport");
        }

        private async void VerNoticiaCompleta(Noticia noticia)
        {
            noticiass = noticia;
            await Shell.Current.GoToAsync("//NoticiaCompleta");
        }


        private async Task SeleccionarImagen()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Seleccionar imagen"
                });

                if (result != null)
                {
                    ImagePath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error al seleccionar la imagen
            }
        }

        public async Task CargarNoticias()
        {
            // Obtener la lista de noticias desde el servicio
            var noticias = await noticiaserver.GetNoticias();

            // Limpiar la lista actual
            NoticiaList.Clear();

            // Agregar las noticias obtenidas a la lista
            noticias.ForEach(x => NoticiaList.Add(x));
            Actualizar(nameof(NoticiaList));

        }
        public async Task CargarUsuario()
        {
            
            var usuarios = await usuarioserver.GetUsuarios();
            UsuarioList.Clear();
            usuarios.ForEach(x => UsuarioList.Add(x));
            Actualizar(nameof(UsuarioList));
        }

        public async void AgregarNoticia()

        {
            
            noticiass.Fecha = DateTime.Now;
            noticiass.Imagen = ConvertImageToBase64(ImagePath);
            await noticiaserver.Insert(noticiass);            
            await Shell.Current.GoToAsync("VerNoticiaReport");
            CargarNoticias();
        }

        private async void VerAgregarRep()
        {
            ImagePath = null;
            await Shell.Current.GoToAsync("//VerAgregarRep");
        }

        private async void VerSesion()
        {
            await Shell.Current.GoToAsync("//LoginView");
        }

        private string ConvertImageToBase64(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return string.Empty;
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void Actualizar(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
