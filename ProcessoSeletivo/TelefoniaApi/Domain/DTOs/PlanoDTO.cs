using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.Entities;

namespace TelefoniaApi.Domain.DTOs
{
    public enum TipoDTO
    {
        Controle = 1, Pos = 2, Pre = 3
    }
    public class PlanoDTO
    {
        public int Id { get; set; }
        public string CodigoPlano { get; set; }
        public int Minutos { get; set; }
        public int FranquiaInternet { get; set; }
        public double Valor { get; set; }
        public TipoDTO Tipo { get; set; }
        public DDDDTO DDD { get; set; }
    }
}
