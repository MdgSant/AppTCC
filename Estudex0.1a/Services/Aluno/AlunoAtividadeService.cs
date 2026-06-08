using EstudeX.Services;
using Estudex0._1a.Models;
using Estudex0._1a.Constants;
using System.Collections.ObjectModel;

namespace Estudex0._1a.Services.Aluno
{
    public class AlunoAtividadeService : Request
    {
        private readonly Request _request;
        private string _token;

        public AlunoAtividadeService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<ObservableCollection<Atividade>> GetAtividadesAsync()
        {
            return await _request.GetAsync<ObservableCollection<Atividade>>(URLsAPI.Atividades, _token);
        }

        public async Task<Atividade> GetAtividadeAsync(int id)
        {
            return await _request.GetAsync<Atividade>($"{URLsAPI.Atividades}/{id}", _token);
        }

        public async Task<AtividadeResposta> PostRespostaAsync(AtividadeResposta resposta)
        {
            return await _request.PostAsync<AtividadeResposta>(URLsAPI.AtividadesRespostas, resposta, _token);
        }
        public async Task<bool> VerificarJaRespondeuAsync(int idAluno, int idAtividade)
        {
            return await _request.GetAsync<bool>(
                $"{URLsAPI.VerificarResposta}/{idAluno}/{idAtividade}", _token);
        }
    }
}