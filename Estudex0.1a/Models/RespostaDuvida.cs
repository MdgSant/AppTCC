using System;
using System.Collections.Generic;
using System.Text;

namespace Estudex0._1a.Models;

public class RespostaDuvida
{
    public int IdDuvida { get; set; }
    public DateTime Momento { get; set; }
    public string ConteudoResposta { get; set; } = string.Empty;
    public int? IdUtilizador { get; set; }       
    public Utilizador? Utilizador { get; set; }  
}
