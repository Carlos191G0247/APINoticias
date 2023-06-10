namespace NoticiaMAUI.Views;

public partial class EditarNoticiaView : ContentPage
{
	public EditarNoticiaView()
	{
		InitializeComponent();
        BindingContext = App.ViewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        
        Imagen12.Opacity = 0.0;
        
    }
    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync("VerNoticiaReport");
        return true; // Indica que se ha manejado el evento de retroceso
    }
}