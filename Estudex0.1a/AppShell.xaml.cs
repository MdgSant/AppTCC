namespace Estudex0._1a;
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        #region Rotas Atividade
        Routing.RegisterRoute("ListarAtividadeView",
            typeof(Estudex0._1a.View.ProfessorViews.AtividadeView));
        Routing.RegisterRoute("InserirAtividadeView",
            typeof(Estudex0._1a.View.ProfessorViews.InserirAtividadeView));
        Routing.RegisterRoute("InserirPerguntasView",
            typeof(Estudex0._1a.View.ProfessorViews.InserirPerguntaView));
        Routing.RegisterRoute("DetalhesAtividadeView",
            typeof(Estudex0._1a.View.ProfessorViews.DetalhesAtividadeView));
        Routing.RegisterRoute("ResponderAtividadeView",
            typeof(Estudex0._1a.View.AlunoViews.ResponderAtividadeView));
        #endregion

        #region Rotas Duvidas
        Routing.RegisterRoute("ListarDuvidaView",
            typeof(Estudex0._1a.View.ProfessorViews.DuvidaView));
        Routing.RegisterRoute("InserirDuvidaView",
            typeof(Estudex0._1a.View.AlunoViews.InserirDuvidaView));
        #endregion
    }

    public void AplicarPerfil()
    {
        int tipo = Preferences.Get("UtilizadorTipo", 0);
        string nome = Preferences.Get("UtilizadorNome", string.Empty);

        bool isProfessor = tipo == 1;
        bool isAluno = tipo == 2;

        LabelPerfil.Text = isProfessor ? $"Professor | {nome}" : $"Aluno | {nome}";

        FlyoutProfAtividades.IsVisible = isProfessor;
        FlyoutProfDuvidas.IsVisible = isProfessor;
        FlyoutAlunoAtividades.IsVisible = isAluno;
        FlyoutAlunoDuvidas.IsVisible = isAluno;
    }
}