using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Entities;
using TelefoniaApi.Domain.Interfaces.Repository;
using TelefoniaApi.Domain.Interfaces.Service;


namespace TelefoniaApi.Domain.Services
{
    public class PlanoService : IPlanoService
    {

        private readonly IPlanoRepository _planoRepository;

        public PlanoService(IPlanoRepository planoRepository)
        {
            this._planoRepository = planoRepository;
        }
        public void DeletePlano(string ddd, string codigoPlano)
        {
            this._planoRepository.DeletePlano(ddd, codigoPlano);
        }

        public List<PlanoDTO> GetAll()
        {
            return _planoRepository.GetAll();
        }

        public List<PlanoDTO> GetPlanoByOperadora(string DDD, string operadora)
        {
            return _planoRepository.GetPlanoByOperadora(DDD, operadora);
        }

        public PlanoDTO GetPlanoByCodigoPlano(string DDD, string codigoPlano)
        {
            return _planoRepository.GetPlanoByCodigoPlano(DDD, codigoPlano);
        }

        public List<PlanoDTO> GetPlanoByTipoPlano(string DDD, DTOs.TipoDTO tipoPlano)
        {
            return _planoRepository.GetPlanoByTipoPlano(DDD, tipoPlano);
        }

        public void SavePlano(PlanoDTO plano)
        {
            _planoRepository.SavePlano(plano);
        }

        public void UpdatePlano(PlanoDTO plano)
        {
            _planoRepository.UpdatePlano(plano);
        }

        public bool ExistPlanoByCodigoPlano(string codigoPlano)
        {
            return _planoRepository.ExistPlanoByCodigoPlano(codigoPlano);
        }
    }
}
