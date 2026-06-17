using APIAcademia.Context;
using APIAcademia.DTOs;
using APIAcademia.DTOs.Alunos;
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
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        // alunos/primeiro
        [HttpGet("primeiro")]
        public async Task<ActionResult<AlunoResponseDTO>> GetPrimeiro()
        {
            // Include carrega Planos para PlanosNome funcionar no DTO 
            var aluno = await _alunoService.ObterPrimeiroAsync();

            return aluno is null ? NotFound("Nenhum aluno cadastrado.") : Ok(aluno);
        }

        // alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoResponseDTO>>> Get() 
            => Ok(await _alunoService.ObterTodosAsync());

        // /alunos/{id}
        [HttpGet("{id:int}", Name = "ObterAluno")]
        public async Task<ActionResult<AlunoResponseDTO>> Get(int id)
        {
            var aluno = await _alunoService.ObterPorIdAsync(id);

            return aluno is null ? NotFound("Nenhum aluno cadastrado.") : Ok(aluno);
        }

        // alunos/filtrar?ativo=true&planoId=1
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<AlunoResponseDTO>>> Filtrar([FromQuery] bool? ativo, [FromQuery] int? planoId)
            => Ok(await _alunoService.FiltrarAsync(ativo, planoId));


        [HttpGet("paginado")]
        public async Task<ActionResult<ResultadoPaginado<AlunoResponseDTO>>> GetPaginado([FromQuery] int pagina = 1, [FromQuery] int itensPorPagina = 10)
        {
            if (pagina < 1 || itensPorPagina < 1 || itensPorPagina > 50)
            {
                return BadRequest("Parâmetros de paginação inválidos. " + "Página >= 1 e itensPorPagina entre 1 e 50.");
            }

            return Ok(await _alunoService.ObterPaginadoAsync(pagina, itensPorPagina));
        }

        // alunos
        [HttpPost]
        public async Task<ActionResult<AlunoResponseDTO>> Post(AlunoRequestDTO dto)
        {
            var criado = await _alunoService.CriarAsync(dto);

            return CreatedAtRoute("ObterAluno", new { id = criado.AlunoId }, criado);
        }

        // alunos/id
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AlunoResponseDTO>> Put(int id, AlunoRequestDTO dto)
        {
            var atualizado = await _alunoService.AtualizarAsync(id, dto);

            return atualizado is null ? NotFound("Aluno não encontrado.") : Ok(atualizado);
        }


        [HttpPatch("{id:int}/status")]
        public async Task<ActionResult<AlunoResponseDTO>> PatchStatus(int id, [FromBody] bool ativo)
        {
            var atualizado = await _alunoService.AtualizarStatusAsync(id, ativo);
            return atualizado is null ? NotFound("Aluno não encontrado.") : Ok(atualizado);
        }

        // alunos/id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var removido = await _alunoService.RemoverAsync(id);

            return removido ? Ok(new { mensagem = "Aluno removido com sucesso." }) : NotFound("Aluno não localizado.");
        }
    }
}