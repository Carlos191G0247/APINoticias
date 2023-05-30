namespace NoticiaMAUI.Views;

public partial class MainView : ContentPage
{
	public MainView()
	{
		InitializeComponent();
		BindingContext = App.ViewModel;
	}
}