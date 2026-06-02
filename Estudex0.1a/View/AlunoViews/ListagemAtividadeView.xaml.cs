using Estudex0._1a.Models;
using Estudex0._1a.ViewModels.ProfessorViewModel;

namespace Estudex0._1a.View.AlunoViews;

public partial class ListagemAtividadeAlunoView : ContentPage
{
    public ListagemAtividadeAlunoView()
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

    private async void OnAtividadeSelecionada(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Atividade atividade)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Shell.Current.GoToAsync($"ResponderAtividadeView?idAtividade={atividade.IdAtividade}");
        }
    }
}