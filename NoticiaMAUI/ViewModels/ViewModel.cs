using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticiaMAUI.ViewModels
{
    public class ViewModel: INotifyPropertyChanged
    {
        public Command VerInicioSesionView { get; set; }
        public ViewModel()
        {
            VerInicioSesionView = new Command(VerSesion);
        }

       

        private async void VerSesion(object obj)
        {
            await Shell.Current.GoToAsync("//LoginView");
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
