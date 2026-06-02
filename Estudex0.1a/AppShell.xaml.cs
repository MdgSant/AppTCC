namespace Estudex0._1a;
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Rotas de navegação para páginas de inserção
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
}