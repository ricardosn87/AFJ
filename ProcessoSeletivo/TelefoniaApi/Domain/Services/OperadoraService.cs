using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Interfaces.Repository;
using TelefoniaApi.Domain.Interfaces.Service;

namespace TelefoniaApi.Domain.Services
{
    public class OperadoraService : IOperadoraService
    {
        private readonly IOperadoraRepository _operadoraRepository;
        public OperadoraService(IOperadoraRepository operadoraRepository)
        {
            this._operadoraRepository = operadoraRepository;
        }
        public void DeleteOperadora(int Id)
        {
            this._operadoraRepository.DeleteOperadora(Id);
        }

        public List<OperadoraDTO> GetAll()
        {
            return _operadoraRepository.GetAll();
        }

        public OperadoraDTO GetByOperadoraById(int Id)
        {
            return _operadoraRepository.GetByOperadoraById(Id);
        }

        public OperadoraDTO GetByOperadoraByNome(string nome)
        {
            return _operadoraRepository.GetByOperadoraByNome(nome);
        }

        public bool GetExistOperadoraWithDDD(int Id)
        {
            return _operadoraRepository.GetExistOperadoraWithDDD(Id);
        }

        public void SaveOperadora(OperadoraDTO operadoraDTO)
        {
            _operadoraRepository.SaveOperadora(operadoraDTO);
        }

        public void UpdateOperadora(OperadoraDTO operadoraDTO)
        {
            _operadoraRepository.UpdateOperadora(operadoraDTO);
        }
    }
}
