using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models
{
    public class AtividadeRascunho
    {
        public string Titulo { get; set; }
        public int PontuacaoMaxima { get; set; }
        public Utilizador Orientador { get; set; }
        public NivelDificuldade NivelDificuldade { get; set; }
        public List<AtividadePergunta> Perguntas { get; set; } = new();
    }
}
