using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelefoniaApi.Domain.ViewsModels
{
    public enum Tipo
    {
        Controle = 1, Pos = 2, Pre = 3
    }
    public class PlanoViewModel
    {      
        public string CodigoPlano { get; set; }
        public int Minutos { get; set; }
        public int FranquiaInternet { get; set; }
        public double Valor { get; set; }
        public Tipo Tipo { get; set; }
        public DDDViewModel DDD { get; set; }
    }
}
