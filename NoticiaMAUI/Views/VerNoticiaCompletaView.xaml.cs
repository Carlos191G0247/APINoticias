using NoticiaMAUI.Service;

namespace NoticiaMAUI.Views;

public partial class VerNoticiaCompletaView : ContentPage
{
	public VerNoticiaCompletaView()
	{
		InitializeComponent();
        BindingContext = App.ViewModel;

    }
    protected override bool OnBackButtonPressed()
    {
        var authService = new AuthService();
        bool isAuthenticated = authService.IsAuthenticated().Result;

        if (isAuthenticated)
        {
            Shell.Current.GoToAsync("VerNoticiaReport");
            
        }
        else
        {
            Shell.Current.GoToAsync("//MainView");

        }
        
        return true; // Indica que se ha manejado el evento de retroceso
    }
}