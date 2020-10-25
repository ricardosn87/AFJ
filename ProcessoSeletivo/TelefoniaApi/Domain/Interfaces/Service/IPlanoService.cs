using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Entities;
using Tipo = TelefoniaApi.Domain.DTOs.TipoDTO;

namespace TelefoniaApi.Domain.Interfaces.Service
{
    public interface IPlanoService
    {
        void SavePlano(PlanoDTO plano);
        void UpdatePlano(PlanoDTO plano);
        void DeletePlano(string ddd, string codigoPlano);
        List<PlanoDTO> GetAll();
        List<PlanoDTO> GetPlanoByTipoPlano(string DDD, Tipo tipoPlano);
        List<PlanoDTO> GetPlanoByOperadora(string DDD, string operadora);
        PlanoDTO GetPlanoByCodigoPlano(string DDD, string codigoPlano);

        bool ExistPlanoByCodigoPlano(string codigoPlano);
    }
}
