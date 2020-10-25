using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;

namespace TelefoniaApi.Domain.Interfaces.Repository
{
    public interface IOperadoraRepository
    {
        List<OperadoraDTO> GetAll();
        OperadoraDTO GetByOperadoraByNome(string nome);
        OperadoraDTO GetByOperadoraById(int Id);
        void UpdateOperadora(OperadoraDTO operadoraDTO);
        void DeleteOperadora(int Id);
        void SaveOperadora(OperadoraDTO operadoraDTO);

        bool GetExistOperadoraWithDDD(int Id);
    }
}
