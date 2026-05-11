using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models
{
    public class AtividadePergunta
    {
        public int IdPergunta { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public int? IdAtividade { get; set; }
        public Atividade? Atividade { get; set; }
    }
}
