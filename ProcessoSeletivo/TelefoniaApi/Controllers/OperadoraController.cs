using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Interfaces.Service;
using TelefoniaApi.Domain.ViewsModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TelefoniaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoraController : ControllerBase
    {
        private readonly IOperadoraService _operadoraService;

        public OperadoraController(IOperadoraService operadoraService)
        {
            this._operadoraService = operadoraService;
        }
        /// <summary>
        /// Metotod que retorna todas as Oepradoras
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_operadoraService.GetAll());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metotod que retorna Opradora pelo seu nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet("GetByOperadora/{nome}")]
        public IActionResult GetByOperadora(string nome)
        {
            try
            {
                return Ok(_operadoraService.GetByOperadoraByNome(nome));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metotod que cria uma Operadora
        /// </summary>
        /// <param name="operadoraViewModel"></param>
        /// <returns></returns>
        [HttpPost("SaveOperadora")]
        public IActionResult SaveOperadora([FromBody] OperadoraViewModel operadoraViewModel)
        {
            try
            {

                var operadoraDTO = new OperadoraDTO
                {
                    Nome = operadoraViewModel.Nome
                };
                var exist = _operadoraService.GetByOperadoraByNome(operadoraDTO.Nome);
                if (exist == null)
                {
                    _operadoraService.SaveOperadora(operadoraDTO);
                    return Created(nameof(SaveOperadora), _operadoraService.GetByOperadoraByNome(operadoraDTO.Nome));
                }

                return BadRequest("Operadora existente.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metotod que atualiza uma Operadora
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operadoraViewModel"></param>
        /// <returns></returns>
        [HttpPut(("UpdateOperadora/{id}"))]
        public IActionResult UpdateOperadora(int id, [FromBody] OperadoraViewModel operadoraViewModel)
        {
            try
            {

                var operadora = _operadoraService.GetByOperadoraById(id);
                if (operadora == null)
                {
                    return NotFound("Operadora não encontrada.");
                }

                operadora = _operadoraService.GetByOperadoraByNome(operadoraViewModel.Nome);
                if (operadora != null)
                {
                    return BadRequest("Operadora existente com o mesmo nome.");
                }

                var operadoraDTO = new OperadoraDTO
                {
                    Nome = operadoraViewModel.Nome,
                    Id = operadora.Id
                };

                _operadoraService.UpdateOperadora(operadoraDTO);

                var operadoraAtualizada = _operadoraService.GetByOperadoraById(id);

                return Ok(operadoraAtualizada);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método que deleta uma Operadora pelo seu ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteOperadora/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var operadora = _operadoraService.GetByOperadoraById(id);
                if (operadora == null)
                {
                    return NotFound("Operadora não encontrada.");
                }

                if (_operadoraService.GetExistOperadoraWithDDD(id))
                    return BadRequest("Não foi possível deletar Operadora, o mesmo se encontra em um ou mais DDDs cadastrados.");

                _operadoraService.DeleteOperadora(id);

                return Ok("Deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
