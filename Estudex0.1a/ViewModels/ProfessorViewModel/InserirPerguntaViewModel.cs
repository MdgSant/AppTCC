using AppRpgEtec.ViewModels;
using Estudex0._1a.Models;
using Estudex0._1a.Services.Professor;
using System.Collections.ObjectModel;

namespace Estudex0._1a.ViewModels.ProfessorViewModel
{
    public class InserirPerguntaViewModel : BaseViewModel
    {
        private ProfessorService aService;

        private int idAtividade;
        private int questaoAtual = 1;
        private const int maxQuestoes = 10;
        private const int maxOpcoes = 5;

        private string enunciado;
        public string Enunciado
        {
            get => enunciado;
            set { enunciado = value; OnPropertyChanged(); }
        }

        private string contadorQuestao;
        public string ContadorQuestao
        {
            get => contadorQuestao;
            set { contadorQuestao = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PerguntasOpcoes> Opcoes { get; set; } = new();

        public Command AdicionarOpcaoCommand { get; set; }
        public Command<PerguntasOpcoes> MarcarCorretaCommand { get; set; }
        public Command<PerguntasOpcoes> RemoverOpcaoCommand { get; set; }
        public Command ProximaQuestaoCommand { get; set; }
        public Command FinalizarAtividadeCommand { get; set; }

        public InserirPerguntaViewModel(int idAtividade)
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            aService = new ProfessorService(token);
            this.idAtividade = idAtividade;

            AtualizarContador();
            AdicionarOpcaoInicial();

            AdicionarOpcaoCommand = new Command(AdicionarOpcao,
                () => Opcoes.Count < maxOpcoes);

            MarcarCorretaCommand = new Command<PerguntasOpcoes>(opcao =>
            {
                foreach (var o in Opcoes) o.Correta = false;
                opcao.Correta = true;
                OnPropertyChanged(nameof(Opcoes));
            });

            RemoverOpcaoCommand = new Command<PerguntasOpcoes>(opcao =>
            {
                if (Opcoes.Count > 1)
                    Opcoes.Remove(opcao);
            });

            ProximaQuestaoCommand = new Command(async () => await SalvarEProximo());
            FinalizarAtividadeCommand = new Command(async () => await SalvarEFinalizar());
        }

        private void AdicionarOpcaoInicial()
        {
            Opcoes.Add(new PerguntasOpcoes { Descricao = "", Correta = false });
        }

        private void AdicionarOpcao()
        {
            if (Opcoes.Count < maxOpcoes)
            {
                Opcoes.Add(new PerguntasOpcoes { Descricao = "", Correta = false });
                ((Command)AdicionarOpcaoCommand).ChangeCanExecute();
            }
        }

        private void AtualizarContador()
        {
            ContadorQuestao = $"{questaoAtual} / {maxQuestoes}";
        }

        private AtividadePergunta MontarPergunta()
        {
            return new AtividadePergunta
            {
                Enunciado = this.enunciado,
                Atividade = new Atividade { IdAtividade = idAtividade },
                Opcoes = Opcoes.ToList()
            };
        }

        private void LimparFormulario()
        {
            Enunciado = string.Empty;
            Opcoes.Clear();
            AdicionarOpcaoInicial();
            ((Command)AdicionarOpcaoCommand).ChangeCanExecute();
        }

        private async Task SalvarEProximo()
        {
            if (!Validar()) return;

            try
            {
                await aService.PostPerguntaAsync(MontarPergunta());

                if (questaoAtual >= maxQuestoes)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Aviso", "Limite de 10 questões atingido!", "Ok");
                    await Shell.Current.GoToAsync("../..");
                    return;
                }

                questaoAtual++;
                AtualizarContador();
                LimparFormulario();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " | " + ex.InnerException, "Ok");
            }
        }

        private async Task SalvarEFinalizar()
        {
            if (!Validar()) return;

            try
            {
                await aService.PostPerguntaAsync(MontarPergunta());

                await Application.Current.MainPage
                    .DisplayAlert("Sucesso", "Atividade finalizada com sucesso!", "Ok");

                await Shell.Current.GoToAsync("../..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " | " + ex.InnerException, "Ok");
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrEmpty(Enunciado))
            {
                Application.Current.MainPage
                    .DisplayAlert("Atenção", "Preencha o enunciado!", "Ok");
                return false;
            }
            if (!Opcoes.Any(o => o.Correta))
            {
                Application.Current.MainPage
                    .DisplayAlert("Atenção", "Marque pelo menos uma opção como correta!", "Ok");
                return false;
            }
            if (Opcoes.Any(o => string.IsNullOrEmpty(o.Descricao)))
            {
                Application.Current.MainPage
                    .DisplayAlert("Atenção", "Preencha todas as alternativas!", "Ok");
                return false;
            }
            return true;
        }
    }
}