using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models.Utilizadores
{
    public class Aluno
    {
        public int idUtilizador {  get; set; }
        public string NomeAluno { get; set; }
        public int idSerie { get; set; }
        public int Xp {  get; set; }
        public Serie Serie { get; set; }
    }
}
