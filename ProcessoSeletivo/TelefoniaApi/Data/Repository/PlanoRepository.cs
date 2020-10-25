using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Entities;
using TelefoniaApi.Domain.Interfaces.Repository;
using TelefoniaApi.Domain.ViewsModels;
using Tipo = TelefoniaApi.Domain.DTOs.TipoDTO;
using TipoEntity = TelefoniaApi.Domain.Entities.Tipo;

namespace TelefoniaApi.Data.Repository
{
    public class PlanoRepository : IPlanoRepository
    {
        private readonly BaseContext _baseContext;
        private readonly IMapper _mapper;
        public PlanoRepository(BaseContext baseContext, IMapper mapper)
        {
            this._baseContext = baseContext;
            _mapper = mapper;
        }
        public void DeletePlano(string ddd, string codigoPlano)
        {
            try
            {
                var p = this._baseContext.Planos.FirstOrDefault(x => x.CodigoPlano == codigoPlano && x.DDD.Numero == ddd);
                if(p != null)
                {
                    this._baseContext.Planos.Remove(p);
                    this._baseContext.SaveChanges();
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PlanoDTO> GetAll()
        {
            try
            {
                var d = this._baseContext.Planos.Include(x => x.DDD).Include(o=>o.DDD.Operadora).ToList();
                return _mapper.Map<List<PlanoDTO>>(d);               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PlanoDTO> GetPlanoByOperadora(string DDD, string nomeOperadora)
        {
            try
            {
                var d = this._baseContext.Planos.Include(x => x.DDD).Include(o => o.DDD.Operadora)
               .Where(x => x.DDD.Numero == DDD && x.DDD.Operadora.Nome.ToLower() == nomeOperadora.ToLower()).ToList();
                return _mapper.Map<List<PlanoDTO>>(d);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }        
        }

        public PlanoDTO GetPlanoByCodigoPlano(string DDD, string codigoPlano)
        {
            try
            {
                var d = this._baseContext.Planos.Include(x => x.DDD).Include(o => o.DDD.Operadora)
                   .FirstOrDefault(x => x.DDD.Numero == DDD && x.CodigoPlano == codigoPlano);

                return _mapper.Map<PlanoDTO>(d);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PlanoDTO> GetPlanoByTipoPlano(string DDD, Tipo tipoPlano)
        {
            try
            {               
                int t = (int)tipoPlano;
                var d = this._baseContext.Planos.Include(x => x.DDD).Include(o => o.DDD.Operadora)
                   .Where(x => x.DDD.Numero == DDD && (int)x.Tipo == t).ToList();
                return _mapper.Map<List<PlanoDTO>>(d);               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SavePlano(PlanoDTO planoDTO)
        {
            try
            {
                var builder = new DbContextOptionsBuilder<BaseContext>();
                builder.UseInMemoryDatabase("Telefone");
                var options = builder.Options;
                using (var baseContext = new BaseContext(options))
                {
                    var operadora = new Operadora
                    {
                        Nome = planoDTO.DDD.Operadora.Nome
                    };

                    baseContext.Operadoras.Add(operadora);

                    var ddd = new DDD
                    {
                        Numero = planoDTO.DDD.Numero,
                        Operadora = operadora
                    };

                    baseContext.DDDs.Add(ddd);

                    var plano = new Plano
                    {
                        CodigoPlano = planoDTO.CodigoPlano,
                        FranquiaInternet = planoDTO.FranquiaInternet,
                        Minutos = planoDTO.Minutos,
                        Tipo = (TipoEntity)(int)planoDTO.Tipo,
                        Valor = planoDTO.Valor,
                        DDD = ddd
                    };

                    baseContext.Planos.Add(plano);

                    baseContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
         
        }

        public void UpdatePlano(PlanoDTO planoDTO)
        {
            try
            {
                var builder = new DbContextOptionsBuilder<BaseContext>();
                builder.UseInMemoryDatabase("Telefone");
                var options = builder.Options;
                using (var baseContext = new BaseContext(options))
                {
                    var plano = this._baseContext.Planos.FirstOrDefault(x => x.CodigoPlano == planoDTO.CodigoPlano);
                    if(plano != null)
                    {

                        var operadora = new Operadora
                        {
                            Nome = planoDTO.DDD.Operadora.Nome
                        };

                        var ddd = new DDD
                        {
                            Numero = planoDTO.DDD.Numero,
                            Operadora = operadora
                        };

                        plano.CodigoPlano = planoDTO.CodigoPlano;
                        plano.FranquiaInternet = planoDTO.FranquiaInternet;
                        plano.Minutos = planoDTO.Minutos;
                        plano.Tipo = (TipoEntity)(int)planoDTO.Tipo;
                        plano.Valor = planoDTO.Valor;

                      

                        plano.DDD = ddd;
                        plano.DDD.Operadora = operadora;
                        this._baseContext.Update(plano);
                        this._baseContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ExistPlanoByCodigoPlano(string codigoPlano)
        {
            try
            {
                return this._baseContext.Planos.Any(x => x.CodigoPlano == codigoPlano);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
