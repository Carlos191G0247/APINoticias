namespace NoticiaMAUI.Views.Login.Admin;

public partial class VerUsuariosView : ContentPage
{
	public VerUsuariosView()
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