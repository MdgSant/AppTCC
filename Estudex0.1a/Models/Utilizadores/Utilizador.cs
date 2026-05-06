using Estudex0._1a.Models.Utilizadores.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models
{
    public class Utilizador
    {
        public int idUtilizador {  get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public byte[] Foto { get; set; }
        public string SenhaHash { get; set; } = string.Empty;
        public TipoUltilizador TipoUltilizador { get; set;}
    }
}
