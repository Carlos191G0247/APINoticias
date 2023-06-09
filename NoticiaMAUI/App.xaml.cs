using NoticiaMAUI.ViewModels;
using NoticiaMAUI.Views;
using NoticiaMAUI.Views.Login;

namespace NoticiaMAUI
{
    public partial class App : Application
    {
        public static ViewModel ViewModel { get; set; } = new();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute("//VerAgregarRep", typeof(AgregarNoticiaView));
            Routing.RegisterRoute("//MainView", typeof(MainView));
            Routing.RegisterRoute("//LoginView", typeof(LoginView));
            Routing.RegisterRoute("//NoticiaCompleta", typeof(VerNoticiaCompletaView));
        }
    }
}