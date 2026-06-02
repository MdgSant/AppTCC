using EstudeX.Services;
using Estudex0._1a.Models;
using Estudex0._1a.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Estudex0._1a.Services.Professor
{
    public class ProfessorDuvidasService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://localhost:8080/duvidas";
        private string _token;

        public ProfessorDuvidasService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<ObservableCollection<Duvida>> GetDuvidasAsync()
        {
            return await _request.GetAsync<ObservableCollection<Duvida>>(apiUrlBase, _token);
        }

        public async Task<Duvida> GetDuvidaAsync(int idDuvida)
        {
            string urlComplementar = string.Format("/{0}", idDuvida);
            return await _request.GetAsync<Duvida>(apiUrlBase + urlComplementar, _token);
        }

        public async Task<Duvida> PostDuvidaAsync(Duvida d)
        {
            return await _request.PostAsync<Duvida>(apiUrlBase, d, _token);
        }

        public async Task<int> PutDuvidaAsync(Duvida d)
        {
            return await _request.PutAsync(apiUrlBase, d, _token);
        }

        public async Task<int> DeleteDuvidaAsync(int idDuvida)
        {
            string urlComplementar = string.Format("/{0}", idDuvida);
            return await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
        }

        public async Task<Duvida> GetDuvidaProfessorAsync(int idDuvida)
        {
            string urlComplementar = string.Format("/professor/{1}", idDuvida);
            return await _request.GetAsync<Duvida>(apiUrlBase + urlComplementar, _token);
        }
    }
}
