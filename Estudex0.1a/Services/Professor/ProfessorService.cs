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

        private const string apiUrlBase = "http://localhost:8080/atividades";

        private string _token;
        public ProfessorService(string token)
        {
            _request = new Request();
            _token = "";
        }
        public async Task<int> PostAtividadeAsync(Atividade a)
        {
            return await _request.PostReturnIntAsync(apiUrlBase, a, _token);
        }

        public async Task<ObservableCollection<Atividade>> GetAtividadesAsync()
        {
            ObservableCollection<Models.Atividade> listaAtividades = await
                _request.GetAsync<ObservableCollection<Models.Atividade>>(apiUrlBase, _token);
            return listaAtividades;
        }

        public async Task<Atividade> GetAtividadeAsync(int atividadeId)
        {
            string urlComplementar = string.Format("/{0}", atividadeId);
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
            string urlComplementar = string.Format("/{0}", atividadeId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }
}
