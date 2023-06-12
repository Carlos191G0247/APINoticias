namespace NoticiaMAUI.Views;

public partial class AgregarNoticiaView : ContentPage
{
	public AgregarNoticiaView()
	{
		InitializeComponent();
        BindingContext = App.ViewModel;
    }
    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync("VerNoticiaReport");
        return true; // Indica que se ha manejado el evento de retroceso
    }
}