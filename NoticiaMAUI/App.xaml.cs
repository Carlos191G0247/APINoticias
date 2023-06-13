using NoticiaMAUI.ViewModels;
using NoticiaMAUI.Views;
using NoticiaMAUI.Views.Login;
using NoticiaMAUI.Views.Login.Reporteros;
using NoticiaMAUI.Views.Login.Admin;
using NoticiaMAUI.Service;

namespace NoticiaMAUI
{
    public partial class App : Application
    {
       
        public static ViewModel ViewModel { get; set; } 
        public App(AuthService auth,LoginService login)
        {
            ViewModel = new ViewModel(auth, login);
            InitializeComponent();
            

            MainPage = new AppShell();
            Routing.RegisterRoute("//VerAgregarRep", typeof(AgregarNoticiaView));
            Routing.RegisterRoute("//MainView", typeof(MainView));
            Routing.RegisterRoute("//LoginView", typeof(LoginView));
            Routing.RegisterRoute("//NoticiaCompleta", typeof(VerNoticiaCompletaView));
            Routing.RegisterRoute("VerNoticiaReport", typeof(VerNotciasView));
            Routing.RegisterRoute("//EditarNoticia", typeof(EditarNoticiaView));
            Routing.RegisterRoute("//VerUsuarios", typeof(VerUsuariosView));
            Routing.RegisterRoute("//VerAgregarUsuario", typeof(VerAgregarUsuarioView));
        }
    }
}