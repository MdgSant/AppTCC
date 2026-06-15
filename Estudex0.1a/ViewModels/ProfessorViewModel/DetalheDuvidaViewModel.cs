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

        EnviarRespostaCommand = new Command(async () => await EnviarResposta());
    }

    private async Task CarregarAsync(int idDuvida)
    {
        try
        {
            Duvida = await _rService.GetDuvidaAsync(idDuvida);
            var respostas = await _rService.GetRespostasDuvidaAsync(idDuvida);
            Respostas.Clear();
            foreach (var r in respostas) Respostas.Add(r);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

    private async Task EnviarResposta()
    {
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
                IdUtilizador = idProf,
                Duvida = new Duvida { IdDuvida = _idDuvida }
            };

            await _dService.PostRespostaAsync(resposta);

            NovaResposta = string.Empty;
            await CarregarAsync(_idDuvida); // recarrega as respostas
            await Application.Current.MainPage
                .DisplayAlert("Sucesso", "Resposta enviada!", "Ok");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Erro", ex.Message, "Ok");
        }
    }
}