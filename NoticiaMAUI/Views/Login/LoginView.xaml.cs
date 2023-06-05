namespace NoticiaMAUI.Views.Login;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
        BindingContext = App.ViewModel;
    }
    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync("//MainView");
        return true; // Indica que se ha manejado el evento de retroceso
    }
}