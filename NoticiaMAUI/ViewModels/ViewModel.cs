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

namespace NoticiaMAUI.ViewModels
{
    public class ViewModel: INotifyPropertyChanged
    {
        public Command VerInicioSesionView { get; set; }

        public Command VerAgregarNoticiaView { get; set; }
        public ICommand SeleccionarImagenCommand { get; }

        public Command AgregarNotciaCommand{get; set; }

        readonly NoticiaService  noticiaserver = new(
            

            );

        private string _imagePath;
        public Noticia noticiass { get; set; } = new Noticia();

        public ObservableCollection<Noticia> NoticiaList { get; set; } = new ObservableCollection<Noticia>();


        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }
        public ViewModel()
        {
            //Para todos
            VerInicioSesionView = new Command(VerSesion);
            //Para Reporteros
            VerAgregarNoticiaView = new Command(VerAgregarRep);
            AgregarNotciaCommand = new Command(AgregarNoticia);
            //imagen
            SeleccionarImagenCommand = new Command(async () => await SeleccionarImagen());

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
                    // Aquí puedes realizar cualquier lógica adicional con la imagen seleccionada
                    // Por ejemplo, puedes guardar la ruta de la imagen en la propiedad ImagePath
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
        }

        public async void AgregarNoticia()
        {

            noticiass.Imagen = ConvertImageToBase64(ImagePath);

            NoticiaList.Add(noticiass);
            await noticiaserver.Insert(noticiass);
            //// Crear una instancia de Noticia con los datos ingresados
            //Noticia nuevaNoticia = new Noticia
            //{
            //    Titulo = noticiass.Titulo,
            //    Autor = noticiass.Autor,
            //    Contenido = noticiass.Contenido,
            //    Imagen = ConvertImageToBase64(ImagePath),
            //    Fecha = DateTime.Now.Date
            //};

            //// Enviar la nueva noticia al servicio para agregarla a la base de datos
            //Noticia noticiaAgregada = await noticiaserver.Insert(nuevaNoticia);

            //// Si se agregó correctamente, agregarla a la lista local
            //if (noticiaAgregada != null && noticiaAgregada.Id > 0)
            //{
            //    NoticiaList.Add(noticiaAgregada);
            //}

            //// Limpiar los campos y restablecer la imagen
            //noticiass = new Noticia();
            //ImagePath = string.Empty;
        }


        private async void VerAgregarRep()
        {
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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
