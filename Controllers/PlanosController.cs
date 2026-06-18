using APIAcademia.Context;
using APIAcademia.DTOs.Planos;
using APIAcademia.Extensions;
using APIAcademia.Models;
using APIAcademia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PlanosController : ControllerBase
    {
        private readonly IPlanoService _planoService;

        public PlanosController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        // planos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoResponseDTO>>> Get()
            => Ok(await _planoService.ObterTodosAsync());

        // planos/{id}
        [HttpGet("{id:int}", Name = "ObterPlano")]
        public async Task<ActionResult<PlanoResponseDTO>> Get(int id)
        {
            var plano = await _planoService.ObterPorIdAsync(id);
            return plano is null ? NotFound($"Plano com ID {id} não encontrado.") : Ok(plano);
        }

        // planos
        [HttpPost]
        public async Task<ActionResult<PlanoResponseDTO>> Post(PlanoRequestDTO dto)
        {
            var criado = await _planoService.CriarAsync(dto);
            return CreatedAtRoute("ObterPlano", new { id = criado.PlanoId }, criado);
        }

        //planos/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PlanoResponseDTO>> Put(int id, PlanoRequestDTO dto)
        {
            var atualizado = await _planoService.AtualizarAsync(id, dto);
            return atualizado is null ? NotFound("Plano não localizado.") : Ok(atualizado);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var removido = await _planoService.RemoverAsync(id);
            return removido ? Ok(new { mensagem = "Plano removido." }) : NotFound("Plano não localizado.");
        }
    }
}
