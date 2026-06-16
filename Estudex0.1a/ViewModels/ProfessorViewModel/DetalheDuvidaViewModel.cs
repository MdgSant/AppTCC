using Estudex0._1a.Models;
using Estudex0._1a.Services.Aluno;
using Estudex0._1a.Services.Professor;
using EstudeX.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Estudex0._1a.ViewModels.ProfessorViewModel;

public class DetalheDuvidaProfessorViewModel : BaseViewModel
{
    private ProfessorDuvidasService _dService;
    private AlunoDuvidaService _rService;
    private int _idDuvida;

    private Duvida duvida;
    public Duvida Duvida
    {
        get => duvida;
        set { duvida = value; OnPropertyChanged(); }
    }

    private string novaResposta = string.Empty;
    public string NovaResposta
    {
        get => novaResposta;
        set { novaResposta = value; OnPropertyChanged(); }
    }

    public bool NaoRespondida => !JaRespondida;

    // E atualize o setter de JaRespondida para notificar também NaoRespondida:
    private bool jaRespondida;
    public bool JaRespondida
    {
        get => jaRespondida;
        set
        {
            jaRespondida = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(NaoRespondida)); // ✅ notifica os dois
        }
    }
    public ObservableCollection<RespostaDuvida> Respostas { get; set; } = new();

    public ICommand EnviarRespostaCommand { get; set; }

    private string idDuvidaStr;
    public string IdDuvidaStr
    {
        get => idDuvidaStr;
        set
        {
            idDuvidaStr = value;
            if (int.TryParse(value, out int id))
            {
                _idDuvida = id;
                _ = CarregarAsync(id);
            }
        }
    }

    public DetalheDuvidaProfessorViewModel()
    {
        string token = Preferences.Get("UsuarioToken", string.Empty);
        _dService = new ProfessorDuvidasService(token);
        _rService = new AlunoDuvidaService(token);

        EnviarRespostaCommand = new Command(
            async () => await EnviarResposta(),
            () => !JaRespondida); // ✅ desabilita se já respondida
    }

    private async Task CarregarAsync(int idDuvida)
    {
        try
        {
            Duvida = await _rService.GetDuvidaAsync(idDuvida);

            var respostas = await _rService.GetRespostasDuvidaAsync(idDuvida);
            Respostas.Clear();
            foreach (var r in respostas) Respostas.Add(r);

            // ✅ Verifica se já foi respondida
            JaRespondida = await _dService.VerificarDuvidaRespondidaAsync(idDuvida);
            ((Command)EnviarRespostaCommand).ChangeCanExecute();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

    private async Task EnviarResposta()
    {
        if (JaRespondida)
        {
            await Application.Current.MainPage
                .DisplayAlert("Aviso", "Esta dúvida já foi respondida!", "Ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(NovaResposta))
        {
            await Application.Current.MainPage
                .DisplayAlert("Atenção", "Digite uma resposta antes de enviar.", "Ok");
            return;
        }

        try
        {
            int idProf = Preferences.Get("UtilizadorId", 0);

            var resposta = new RespostaDuvida
            {
                ConteudoResposta = NovaResposta,
                Momento = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                Utilizador = new Utilizador { IdUtilizador = idProf },
                Duvida = new Duvida { IdDuvida = _idDuvida }
            };

            bool sucesso = await _dService.PostRespostaDuvidaAsync(resposta);

            if (sucesso)
            {
                NovaResposta = string.Empty;
                JaRespondida = true;
                ((Command)EnviarRespostaCommand).ChangeCanExecute();
                await CarregarAsync(_idDuvida);
                await Application.Current.MainPage
                    .DisplayAlert("Sucesso", "Resposta enviada!", "Ok");
            }
            else
            {
                await Application.Current.MainPage
                    .DisplayAlert("Erro", "Não foi possível enviar a resposta.", "Ok");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Erro", ex.Message, "Ok");
        }
    }
}