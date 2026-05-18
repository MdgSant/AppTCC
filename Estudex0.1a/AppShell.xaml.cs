using Estudex0._1a.View.ProfessorViews;

namespace Estudex0._1a;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(InserirAtividadeView), typeof(InserirAtividadeView));
    }
}