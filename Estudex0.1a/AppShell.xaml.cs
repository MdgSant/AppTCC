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
        #endregion
        Routing.RegisterRoute("ListarDuvidaView",
            typeof(Estudex0._1a.View.ProfessorViews.DuvidaView));
        Routing.RegisterRoute("InserirDuvidaView",
            typeof(Estudex0._1a.View.AlunoViews.InserirDuvidaView));
    }
}