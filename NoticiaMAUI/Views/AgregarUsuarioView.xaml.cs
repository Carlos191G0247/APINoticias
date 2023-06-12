namespace NoticiaMAUI.Views;

public partial class AgregarUsuarioView : ContentPage
{
	public AgregarUsuarioView()
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