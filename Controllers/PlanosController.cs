using APIAcademia.Context;
using APIAcademia.DTOs.Planos;
using APIAcademia.Extensions;
using APIAcademia.Models;
using APIAcademia.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlanosController : ControllerBase
    {
        private readonly IPlanoService _planoService;

        public PlanosController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlanoResponseDTO>> Get()
            => Ok(_planoService.ObterTodos());

        [HttpGet("{id:int}", Name = "ObterPlano")]
        public ActionResult<PlanoResponseDTO> Get(int id)
        {
            var plano = _planoService.ObterPorId(id);
            return plano is null ? NotFound($"Plano com ID {id} não encontrado.") : Ok(plano);
        }

        [HttpPost]
        public ActionResult<PlanoResponseDTO> Post(PlanoRequestDTO dto)
        {
            var criado = _planoService.Criar(dto);
            return CreatedAtRoute("ObterPlano", new { id = criado.PlanoId }, criado);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PlanoResponseDTO> Put(int id, PlanoRequestDTO dto)
        {
            var atualizado = _planoService.Atualizar(id, dto);
            return atualizado is null ? NotFound("Plano não localizado.") : Ok(atualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var removido = _planoService.Remover(id);
            return removido ? Ok(new { mensagem = "Plano removido." }) : NotFound("Plano não localizado.");
        }
    }
}
