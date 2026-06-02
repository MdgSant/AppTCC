using EstudeX.Services;
using Estudex0._1a.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Estudex0._1a.Services.Professor
{
    public class ProfessorService : Request
    {
        private readonly Request _request;
        #region Conexão API - caminhos
        private const string apiUrlBase = "http://localhost:8080/atividades";
        private const string apiUrlBaseNivel = "http://localhost:8080/niveldificuldade";
        private const string apiUrlBaseUtilizador = "http://localhost:8080/utilizadores";
        private const string apiUrlBaseDisciplina = "http://localhost:8080/disciplinas";
        private const string apiUrlBasePergunta = "http://localhost:8080/atividadesPergunta";
        #endregion
        public async Task<ObservableCollection<NivelDificuldade>> GetNiveisDificuldadeAsync()
        {
            return await _request.GetAsync<ObservableCollection<NivelDificuldade>>(apiUrlBaseNivel, _token);
        }
        public async Task<ObservableCollection<Utilizador>> GetOrientadoresAsync()
        {
            return await _request.GetAsync<ObservableCollection<Utilizador>>(apiUrlBaseUtilizador, _token);
        }
        public async Task<ObservableCollection<Disciplina>> GetDisciplinasAsync()
        {
            return await _request.GetAsync<ObservableCollection<Disciplina>>(apiUrlBaseDisciplina, _token);
        }

        private string _token;
        public ProfessorService(string token)
        {
            _request = new Request();
            _token = "";
        }
        public async Task<Atividade> PostAtividadeAsync(Atividade a)
        {
            return await _request.PostAsync<Atividade>(apiUrlBase, a, _token);
        }
        #region Métodos Post
        public async Task<ObservableCollection<Atividade>> GetAtividadesAsync()
        {
            ObservableCollection<Models.Atividade> listaAtividades = await
                _request.GetAsync<ObservableCollection<Models.Atividade>>(apiUrlBase, _token);
            return listaAtividades;
        }

        public async Task<AtividadePergunta> PostPerguntaAsync(AtividadePergunta p)
        {
            return await _request.PostAsync<AtividadePergunta>(apiUrlBasePergunta, p, _token);
        }
        #endregion
        public async Task<Atividade> GetAtividadeAsync(int atividadeId)
        {
            string urlComplementar = string.Format("/{1}", atividadeId);
            var atividade = await _request.GetAsync<Models.Atividade>(apiUrlBase + urlComplementar, _token);
            return atividade;
        }

        public async Task<int> PutAtividadeAsync(Atividade a)
        {
            var result = await _request.PutAsync(apiUrlBase, a, _token);
            return result;
        }

        public async Task<int> DeleteAtividadeAsync(int atividadeId)
        {
            string urlComplementar = string.Format("/{1}", atividadeId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }
}
