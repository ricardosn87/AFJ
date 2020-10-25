using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TelefoniaApi.Domain.Entities
{
    public enum Tipo : int
    {
        Controle = 1, Pos = 2, Pre = 3
    }
    public class Plano
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CodigoPlano { get; set; }
        public int Minutos { get; set; }
        public int FranquiaInternet { get; set; }
        public double Valor { get; set; }
        public Tipo Tipo { get; set; }
        public DDD DDD { get; set; }
    }
}
