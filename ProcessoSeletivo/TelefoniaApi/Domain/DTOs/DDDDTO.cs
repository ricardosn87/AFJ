using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.Entities;

namespace TelefoniaApi.Domain.DTOs
{
    public class DDDDTO
    {
        public int Id { get; set; }
        public string Numero { get; set; }

        public OperadoraDTO Operadora { get; set; }


    }
}
