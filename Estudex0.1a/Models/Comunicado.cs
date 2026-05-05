using Estudex0._1a.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models
{
     public class Comunicado
    {
        public int idPergunta {  get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public int idAtividade { get; set; }
        public Atividade Atividade { get; set; }
    }
}
