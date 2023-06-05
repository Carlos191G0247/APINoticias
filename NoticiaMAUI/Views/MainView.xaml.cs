namespace NoticiaMAUI.Views;

public partial class MainView : ContentPage
{
	public MainView()
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