using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Constants
{
    public class URLsAPI
    {
        private const string BaseUrl = "http://localhost:8080";

        public const string Atividades = $"{BaseUrl}/atividades";
        public const string NivelDificuldade = $"{BaseUrl}/nivelDificuldade";
        public const string Utilizadores = $"{BaseUrl}/utilizadores";
        public const string Disciplinas = $"{BaseUrl}/disciplinas";
        public const string AtividadesPergunta = $"{BaseUrl}/atividadesPergunta";
        public const string Duvidas = $"{BaseUrl}/duvidas";
        public const string NiveisDificuldade = $"{BaseUrl}/nivelDificuldade";
        public const string AtividadesRespostas = $"{BaseUrl}/atividadesrespostas";
    }
}
