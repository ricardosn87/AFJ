using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;

namespace TelefoniaApi.Domain.Interfaces.Service
{
    public  interface IDDDService
    {
        List<DDDDTO> GetAll();
        DDDDTO GetByDDDById(int Id);
        DDDDTO GetByDDDByNumero(string numero);
        void UpdateDDD(DDDDTO  dDDDTO);
        void DeleteDDD(int Id);
        void SaveDDD(DDDDTO dDDDTO);

        bool GetExistDDDWithPlano(int Id);
    }
}
