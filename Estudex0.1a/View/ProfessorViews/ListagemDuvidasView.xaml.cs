using Estudex0._1a.ViewModels.ProfessorViewModel;

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

    private async void OnNovaDuvidaClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("InserirDuvidaView");
    }
}