namespace NoticiaMAUI.Views.Login.Admin;

public partial class VerAgregarUsuarioView : ContentPage
{
	public VerAgregarUsuarioView()
	{
		InitializeComponent();
        BindingContext = App.ViewModel;
    }
    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync("//VerUsuarios");
        return true; // Indica que se ha manejado el evento de retroceso
    }
}