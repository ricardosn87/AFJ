using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Entities;
using TelefoniaApi.Domain.Interfaces.Repository;

namespace TelefoniaApi.Data.Repository
{
    public class OperadoraRepository : IOperadoraRepository
    {
        private readonly BaseContext _baseContext;
        private readonly IMapper _mapper;

        public OperadoraRepository(BaseContext baseContext, IMapper mapper)
        {
            this._baseContext = baseContext;
            _mapper = mapper;
        }
        public void DeleteOperadora(int Id)
        {
            try
            {
                var ope = _baseContext.Operadoras.FirstOrDefault(x => x.Id == Id);
                _baseContext.Operadoras.Remove(ope);
                _baseContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<OperadoraDTO> GetAll()
        {
            try
            {
                var d = this._baseContext.Operadoras.ToList();
                return _mapper.Map<List<OperadoraDTO>>(d);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OperadoraDTO GetByOperadoraByNome(string nome)
        {
            try
            {
                var d = this._baseContext.Operadoras.FirstOrDefault(x => x.Nome.ToLower() == nome.ToLower());
                return _mapper.Map<OperadoraDTO>(d);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OperadoraDTO GetByOperadoraById(int Id)
        {
            try
            {
                var d = this._baseContext.Operadoras.FirstOrDefault(x => x.Id == Id);
                return _mapper.Map<OperadoraDTO>(d);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveOperadora(OperadoraDTO operadoraDTO)
        {
            try
            {
                var operadora = new Operadora
                {
                    Nome = operadoraDTO.Nome
                };
                this._baseContext.Operadoras.Add(operadora);
                this._baseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOperadora(OperadoraDTO operadoraDTO)
        {
            try
            {
                var builder = new DbContextOptionsBuilder<BaseContext>();
                builder.UseInMemoryDatabase("Telefone");
                var options = builder.Options;

                using (var baseContext = new BaseContext(options))
                {
                    var ope = baseContext.Operadoras.FirstOrDefault(x => x.Id == operadoraDTO.Id);

                    if (ope != null)
                    {
                        ope.Nome = operadoraDTO.Nome;
                        baseContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool GetExistOperadoraWithDDD(int Id)
        {
            try
            {
                return this._baseContext.DDDs.Include(x => x.Operadora).Any(x => x.Operadora.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
