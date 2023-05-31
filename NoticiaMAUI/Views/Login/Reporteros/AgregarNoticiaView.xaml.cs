namespace NoticiaMAUI.Views;

public partial class AgregarNoticiaView : ContentPage
{
	public AgregarNoticiaView()
	{
		InitializeComponent();
        BindingContext = App.ViewModel;
    }
}