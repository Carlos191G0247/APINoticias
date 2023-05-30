namespace NoticiaMAUI.Views;

public partial class InicioView : ContentPage
{
	public InicioView()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("//MainView");
    }
}