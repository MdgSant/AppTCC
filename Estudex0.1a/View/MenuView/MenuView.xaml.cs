using Estudex0._1a.View.Utilizador;

namespace Estudex0._1a.View;
public partial class MenuView : ContentPage
{
    public MenuView()
    {
        InitializeComponent();
        AplicarPerfil();
    }

    private void AplicarPerfil()
    {
        int tipo = Preferences.Get("UtilizadorTipo", 0);
        string nome = Preferences.Get("UtilizadorNome", string.Empty);

        LabelNome.Text = nome;
        LabelPerfil.Text = tipo == 1 ? "Professor" : "Aluno";

        MenuProfessor.IsVisible = tipo == 2;
        MenuAluno.IsVisible = tipo == 1;
    }

    // Professor
    private async void OnAtividadesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AtividadeView");
    }

    private async void OnDuvidasClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ListagemDuvidasView");
    }

    // Aluno
    private async void OnAtividadesAlunoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ListagemAtividadeView");
    }

    private async void OnDuvidasAlunoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ListagemDuvidasAlunoView");
    }

    private async void OnDuvidasGeraisClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ListagemDuvidasAlunoView");
    }

    // Logout
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        Preferences.Clear();
        Application.Current.MainPage = new NavigationPage(new LoginView());
    }
}
