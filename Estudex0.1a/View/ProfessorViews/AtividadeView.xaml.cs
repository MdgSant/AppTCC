using Estudex0._1a.ViewModels.ProfessorViewModel;

namespace Estudex0._1a.View.ProfessorViews;

public partial class AtividadeView : ContentPage
{
	public AtividadeView()
	{
		InitializeComponent();

        BindingContext = new ListagemAtividadeViewModel();
    }
}