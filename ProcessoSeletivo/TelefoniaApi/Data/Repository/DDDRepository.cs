using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Entities;
using TelefoniaApi.Domain.Interfaces.Repository;

namespace TelefoniaApi.Data.Repository
{
    public class DDDRepository : IDDDRepository
    {
        private readonly BaseContext _baseContext;
        private readonly IMapper _mapper;

        public DDDRepository(BaseContext baseContext, IMapper mapper)
        {
            this._baseContext = baseContext;
            _mapper = mapper;
        }
        public void DeleteDDD(int Id)
        {
            try
            {
                var ddd = _baseContext.DDDs.FirstOrDefault(x => x.Id == Id);
                _baseContext.DDDs.Remove(ddd);
                _baseContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<DDDDTO> GetAll()
        {
            try
            {
                var d = this._baseContext.DDDs.Include(x => x.Operadora).ToList();
                return _mapper.Map<List<DDDDTO>>(d);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DDDDTO GetByDDDById(int Id)
        {
            try
            {
                var d = this._baseContext.DDDs.FirstOrDefault(x => x.Id == Id);
                return _mapper.Map<DDDDTO>(d);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DDDDTO GetByDDDByNumero(string numero)
        {
            try
            {
                var d = this._baseContext.DDDs.FirstOrDefault(x => x.Numero == numero);
                return _mapper.Map<DDDDTO>(d);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool GetExistDDDWithPlano(int Id)
        {
            try
            {
                return this._baseContext.Planos.Include(x => x.DDD).Any(x => x.DDD.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveDDD(DDDDTO dDDDTO)
        {
            try
            {
                var ddd = new DDD
                {
                    Numero = dDDDTO.Numero
                };
                this._baseContext.DDDs.Add(ddd);
                this._baseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateDDD(DDDDTO dDDDTO)
        {
            try
            {
                var builder = new DbContextOptionsBuilder<BaseContext>();
                builder.UseInMemoryDatabase("Telefone");
                var options = builder.Options;

                using (var baseContext = new BaseContext(options))
                {
                    var ddd = baseContext.DDDs.FirstOrDefault(x => x.Id == dDDDTO.Id);

                    if (ddd != null)
                    {
                        ddd.Numero = dDDDTO.Numero;
                        baseContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
