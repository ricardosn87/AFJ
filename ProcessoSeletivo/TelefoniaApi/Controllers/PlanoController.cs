using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Interfaces.Service;
using TelefoniaApi.Domain.ViewsModels;
using Tipo = TelefoniaApi.Domain.ViewsModels.Tipo;


namespace TelefoniaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoService _iPlanoService;
        private readonly IDDDService _dDDService;
        private readonly IOperadoraService _operadoraService;

        public PlanoController(IPlanoService iPlanoTelefoniaService, IDDDService dDDService, IOperadoraService operadoraService)
        {
            this._iPlanoService = iPlanoTelefoniaService;
            this._dDDService = dDDService;
            this._operadoraService = operadoraService;
        }

        /// <summary>
        /// Metodo que retorna todos os Planos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var d = _iPlanoService.GetAll();
                return Ok(d);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metodo que retorna Plano pelo seu DDD e por Tipo (Controle,Pre ou Pos) 
        /// </summary>
        /// <param name="DDD"></param>
        /// <param name="tipoplano"></param>
        /// <returns></returns>
        [HttpGet("GetByTipoPlano/{DDD}/{tipoplano}")]
        public IActionResult GetByTipoPlano(string DDD, Tipo tipoplano)
        {
            try
            {
                TipoDTO _tipo = (TipoDTO)(int)tipoplano;
                var d = _iPlanoService.GetPlanoByTipoPlano(DDD, _tipo);
                return Ok(d);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metodo que retorna Plano pelo seu DDD e Código
        /// </summary>
        /// <param name="DDD"></param>
        /// <param name="codigoPlano"></param>
        /// <returns></returns>
        [HttpGet("GetByCodigoPlano/{DDD}/{codigoPlano}")]
        public IActionResult GetByCodigoPlano(string DDD, string codigoPlano)
        {
            try
            {
                var d = _iPlanoService.GetPlanoByCodigoPlano(DDD, codigoPlano);
                return Ok(d);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///  Metodo que retorna Plano pelo seu DDD e Operadora
        /// </summary>
        /// <param name="DDD"></param>
        /// <param name="operadora"></param>
        /// <returns></returns>
        [HttpGet("GetByOperadora/{DDD}/{operadora}")]
        public IActionResult GetByOperadora(string DDD, string operadora)
        {
            try
            {
                var d = _iPlanoService.GetPlanoByOperadora(DDD, operadora);
                return Ok(d);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metodo que cria um Plano novo
        /// </summary>
        /// <param name="planoViewModel"></param>
        /// <returns></returns>
        [HttpPost("SavePlano")]
        public IActionResult SavePlano([FromBody] PlanoViewModel planoViewModel)
        {
            try
            {

                var ddddExist = _dDDService.GetByDDDByNumero(planoViewModel.DDD.Numero);
                if (ddddExist == null)
                {
                    return BadRequest("DDD não cadastrado.");
                }
                var opeExist = this._operadoraService.GetByOperadoraByNome(planoViewModel.DDD.Operadora.Nome);
                if (opeExist == null)
                {
                    return BadRequest("Operadora não cadastrada.");
                }

                var planoExist = _iPlanoService.GetPlanoByCodigoPlano(planoViewModel.DDD.Numero, planoViewModel.CodigoPlano);
                if (planoExist != null)
                {
                    return BadRequest("Plano já cadastrado.");
                }

                var planoDTO = new PlanoDTO
                {
                    CodigoPlano = planoViewModel.CodigoPlano,
                    FranquiaInternet = planoViewModel.FranquiaInternet,
                    Minutos = planoViewModel.Minutos,
                    Tipo = (TipoDTO)(int)planoViewModel.Tipo,
                    Valor = planoViewModel.Valor,
                    DDD = new DDDDTO
                    {
                        Numero = planoViewModel.DDD.Numero,
                        Operadora = new OperadoraDTO
                        {
                            Nome = planoViewModel.DDD.Operadora.Nome
                        }
                    }
                };

                _iPlanoService.SavePlano(planoDTO);

                return Created(nameof(SavePlano), _dDDService.GetByDDDByNumero(planoViewModel.DDD.Numero));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metodo que atualiza um Plano
        /// </summary>
        /// <param name="id"></param>
        /// <param name="planoViewModel"></param>
        /// <returns></returns>
        [HttpPut("UpdatePlano/{id}")]
        public IActionResult UpdatePlano(int id, [FromBody] PlanoViewModel planoViewModel)
        {

            try
            {
                var ddddExist = _dDDService.GetByDDDByNumero(planoViewModel.DDD.Numero);
                if (ddddExist == null)
                {
                    return BadRequest("DDD não cadastrado.");
                }
                var opeExist = this._operadoraService.GetByOperadoraByNome(planoViewModel.DDD.Operadora.Nome);
                if (opeExist == null)
                {
                    return BadRequest("Operadora não cadastrada.");
                }

                bool planoExist = this._iPlanoService.ExistPlanoByCodigoPlano(planoViewModel.CodigoPlano);
                if (!planoExist)
                {
                    return BadRequest("Plano Não cadastrado.");
                }

                var planoDTO = new PlanoDTO
                {
                    CodigoPlano = planoViewModel.CodigoPlano,
                    FranquiaInternet = planoViewModel.FranquiaInternet,
                    Minutos = planoViewModel.Minutos,
                    Tipo = (TipoDTO)(int)planoViewModel.Tipo,
                    Valor = planoViewModel.Valor,
                    DDD = new DDDDTO
                    {
                        Numero = planoViewModel.DDD.Numero,
                        Operadora = new OperadoraDTO
                        {
                            Nome = planoViewModel.DDD.Operadora.Nome
                        }
                    }
                };

                this._iPlanoService.UpdatePlano(planoDTO);

                return Ok(this._iPlanoService.GetPlanoByCodigoPlano(ddddExist.Numero, planoViewModel.CodigoPlano));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metodo que deleta um Plano
        /// </summary>
        /// <param name="ddd"></param>
        /// <param name="codigoPlano"></param>
        /// <returns></returns>
        [HttpDelete("DeletePlano/{ddd}/{codigoPlano}")]
        public IActionResult DeletePlano(string ddd, string codigoPlano)
        {
            try
            {
                var plano = this._iPlanoService.GetPlanoByCodigoPlano(ddd, codigoPlano);
                if (plano == null)
                    return BadRequest("Plano não existe");

                this._iPlanoService.DeletePlano(ddd, codigoPlano);

                return Ok("Deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
