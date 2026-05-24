using Estudex0._1a.ViewModels.ProfessorViewModel;

namespace Estudex0._1a.View.ProfessorViews;

public partial class AtividadeView : ContentPage
{
    public AtividadeView()
    {
        InitializeComponent();

        BindingContext = new ListagemAtividadeViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = BindingContext as ListagemAtividadeViewModel;
        if (vm != null)
            await vm.ObterAtividades();
    }

    private async void OnNovaAtividadeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(InserirAtividadeView));
    }
}