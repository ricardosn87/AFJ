using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Interfaces.Service;
using TelefoniaApi.Domain.ViewsModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TelefoniaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class DDDController : ControllerBase
    {
        private readonly IDDDService _dDDService;

        public DDDController(IDDDService dDDService)
        {
            this._dDDService = dDDService;
        }
       

        /// <summary>
        /// Metotod que retona todos DDDs
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_dDDService.GetAll());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metotod que retorna DDD pelo seu numero
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        [HttpGet("GetByDDDByNumero/{numero}")]
        public IActionResult GetByDDDByNumero(string numero)
        {
            try
            {
                return Ok(_dDDService.GetByDDDByNumero(numero));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metoto que cria um DDD
        /// </summary>
        /// <param name="dDDViewModel"></param>
        /// <returns></returns>
        [HttpPost("SaveDDD")]
        public IActionResult SaveDDD([FromBody] DDDViewModel dDDViewModel)
        {
            try
            {

                var dddDTO = new DDDDTO
                {
                    Numero = dDDViewModel.Numero
                };
                var exist = _dDDService.GetByDDDByNumero(dddDTO.Numero);
                if (exist == null)
                {
                    _dDDService.SaveDDD(dddDTO);
                    return Created(nameof(SaveDDD), _dDDService.GetByDDDByNumero(dddDTO.Numero));
                }

                return BadRequest("DDD existente.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metotod que atualiza DDD 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dDDViewModel"></param>
        /// <returns></returns>
        [HttpPut("UpdateDDD/{id}")]
        public IActionResult UpdateDDD(int id, [FromBody] DDDViewModel dDDViewModel)
        {
            try
            {

                var ddd = _dDDService.GetByDDDById(id);
                if (ddd == null)
                {
                    return NotFound("DDD não encontrado.");
                }

                ddd = _dDDService.GetByDDDByNumero(dDDViewModel.Numero);
                if (ddd != null)
                {
                    return BadRequest("DDD existente com o mesmo Numero.");
                }

                var dddDTO = new DDDDTO
                {
                    Numero = dDDViewModel.Numero,
                    Id = id
                };

                _dDDService.UpdateDDD(dddDTO);

                return Ok(_dDDService.GetByDDDById(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metotod que deleta DDD pelo seu ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDDD/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                var ddd = _dDDService.GetByDDDById(id);
                if (ddd == null)
                {
                    return NotFound("DDD não encontrado.");
                }

                if (_dDDService.GetExistDDDWithPlano(id))
                    return BadRequest("Não foi possível deletar DDD, o mesmo se encontra em um ou mais Planos cadastrados.");

                _dDDService.DeleteDDD(id);

                return Ok("Deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
