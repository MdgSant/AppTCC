using Estudex0._1a.ViewModels.ProfessorViewModel;

namespace Estudex0._1a.View.AlunoViews;

public partial class ListagemDuvidasAlunoView : ContentPage
{
    public ListagemDuvidasAlunoView()
    {
        InitializeComponent();
    }
	
    public DuvidaView()
    {
        InitializeComponent();
        BindingContext = new ListagemDuvidaViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = BindingContext as ListagemDuvidaViewModel;
        if (vm != null)
            await vm.InicializarAsync();
    }

    private async void OnNovaDuvidaClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("InserirDuvidaView");
    }

    private async void OnBuscarPorIdClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as ListagemDuvidaViewModel;
        if (vm == null) return;

        if (int.TryParse(EntryIdDuvida.Text, out int idDuvida))
        {
            await vm.ObterDuvidaPorId(idDuvida);
        }
        else
        {
            await DisplayAlert("Ops", "Informe um ID válido.", "Ok");
        }
    }
}
