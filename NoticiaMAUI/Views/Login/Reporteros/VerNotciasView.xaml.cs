namespace NoticiaMAUI.Views.Login.Reporteros;

public partial class VerNotciasView : ContentPage
{
	public VerNotciasView()
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