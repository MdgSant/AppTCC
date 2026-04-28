using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models
{
    public class Usuario
    {
        public int idUtilizador {  get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public byte[] Foto { get; set; }
        public string SenhaHash {  get; set; }
    }
}
