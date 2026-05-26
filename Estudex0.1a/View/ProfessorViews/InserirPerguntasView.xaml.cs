using Estudex0._1a.ViewModels.ProfessorViewModel;

namespace Estudex0._1a.View.ProfessorViews;

[QueryProperty(nameof(IdAtividade), "idAtividade")]
public partial class InserirPerguntaView : ContentPage
{
    private int _idAtividade;
    public int IdAtividade
    {
        set
        {
            _idAtividade = value;
            BindingContext = new InserirPerguntaViewModel(_idAtividade);
        }
    }

    public InserirPerguntaView()
    {
        InitializeComponent();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("../..");
    }
}