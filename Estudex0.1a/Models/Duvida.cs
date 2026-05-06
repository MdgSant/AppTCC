using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models
{
    public class Duvida
    {
        public int IdDuvida { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime Momento { get; set; }
        public string StatusDuvida { get; set; } = string.Empty;
        public int? IdUtilizador { get; set; }      
        public Utilizador? Utilizador { get; set; }  
        public int? IdConteudo { get; set; }         
        public Conteudo? Conteudo { get; set; }
    }
}
