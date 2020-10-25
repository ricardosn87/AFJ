using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Interfaces.Repository;
using TelefoniaApi.Domain.Interfaces.Service;

namespace TelefoniaApi.Domain.Services
{
    public class DDDService : IDDDService
    {
        private readonly IDDDRepository _dDDRepository;
        public DDDService(IDDDRepository dDDRepository)
        {
            this._dDDRepository = dDDRepository;
        }
        public void DeleteDDD(int Id)
        {
            _dDDRepository.DeleteDDD(Id);
        }

        public List<DDDDTO> GetAll()
        {
            return _dDDRepository.GetAll();
        }

        public DDDDTO GetByDDDById(int Id)
        {
            return _dDDRepository.GetByDDDById(Id);
        }

        public DDDDTO GetByDDDByNumero(string numero)
        {
            return _dDDRepository.GetByDDDByNumero(numero);
        }

        public bool GetExistDDDWithPlano(int Id)
        {
            return _dDDRepository.GetExistDDDWithPlano(Id);
        }

        public void SaveDDD(DDDDTO dDDDTO)
        {
            _dDDRepository.SaveDDD(dDDDTO);
        }

        public void UpdateDDD(DDDDTO dDDDTO)
        {
            _dDDRepository.UpdateDDD(dDDDTO);
        }
    }
}
