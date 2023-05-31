using NoticiaMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoticiaMAUI.ViewModels
{
    public class ViewModel: INotifyPropertyChanged
    {
        public Command VerInicioSesionView { get; set; }

        public Command VerAgregarNoticiaView { get; set; }
        public ICommand SeleccionarImagenCommand { get; }
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

        private async void VerAgregarRep()
        {
            await Shell.Current.GoToAsync("//VerAgregarRep");
        }

        private async void VerSesion()
        {
            await Shell.Current.GoToAsync("//LoginView");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
