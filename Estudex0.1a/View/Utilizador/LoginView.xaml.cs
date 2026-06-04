using Estudex0._1a.ViewModels.UtilizadoresViewModel;

namespace Estudex0._1a.View.Utilizador;

public partial class LoginView : ContentPage
{
	UtilizadorViewModel utilizadorViewModel;
	public LoginView()
	{
		InitializeComponent();

		utilizadorViewModel = new UtilizadorViewModel();
		BindingContext = utilizadorViewModel;
	}
}