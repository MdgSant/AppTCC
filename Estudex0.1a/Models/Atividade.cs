using Estudex0._1a.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models
{
    public class Atividade
    {
        public int idAtividade {  get; set; }
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public int PontucaoMaxima { get; set; }
        public int IdOrientador { get; set; }

    }
}
