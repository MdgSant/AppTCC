using Estudex0._1a.ViewModels.ProfessorViewModel;
using Estudex0._1a.Models;

namespace Estudex0._1a.View.ProfessorViews;

public partial class DuvidaView : ContentPage
{
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

    // ?? MÉTODO PARA CLICAR NO CARD ??
    private async void OnDuvidaTapped(object sender, TappedEventArgs e)
    {
        var vm = BindingContext as ListagemDuvidaViewModel;
        if (vm == null) return;

        var frame = sender as Frame;
        var duvida = frame?.BindingContext as Duvida;

        if (duvida != null)
        {
            await vm.ObterDuvidaPorId(duvida.IdDuvida);
        }
    }
}