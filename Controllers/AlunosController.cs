using APIAcademia.Context;
using APIAcademia.DTOs.Alunos;
using APIAcademia.Extensions;
using APIAcademia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Controllers

{
    [Route("[controller]")]
    [ApiController]

    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        // alunos/primeiro
        [HttpGet("primeiro")]
        public ActionResult<AlunoResponseDTO> GetPrimeiro()
        {
            // Include carrega Planos para PlanosNome funcionar no DTO 
            var aluno = _context.Alunos.Include(a => a.Planos).AsNoTracking().FirstOrDefault();

            if (aluno is null)
            {
                return NotFound("Nenhum aluno cadastrado.");
            }

            return Ok(aluno.ToResponseDTO());
        }

        // alunos
        [HttpGet]
        public ActionResult<IEnumerable<AlunoResponseDTO>> Get()
        {
            var alunos = _context.Alunos.Include(a => a.Planos).AsNoTracking().ToList();

            if (alunos is null)
            {
                return NotFound("Nenhum aluno cadastrado.");
            }

            return Ok(alunos.Select(a => a.ToResponseDTO()));
        }

        // /alunos/id
        [HttpGet("{id:int}", Name = "ObterAluno")]
        public ActionResult<AlunoResponseDTO> Get(int id)
        {
            var aluno = _context.Alunos.Include(a => a.Planos).AsNoTracking().FirstOrDefault(a => a.AlunoId == id);

            if (aluno is null)
            {
                return NotFound("Aluno não encontrado.");
            }

            return Ok(aluno.ToResponseDTO());
        }

        // alunos/filtrar?ativo=true&planoId=1
        [HttpGet("filtrar")]
        public ActionResult<IEnumerable<AlunoResponseDTO>> Filtrar([FromQuery] bool? ativo, [FromQuery] int? planoId) 
        {
            var query = _context.Alunos.Include(a => a.Planos).AsNoTracking().AsQueryable();

            if (ativo.HasValue)
                query = query.Where(a => a.Ativo == ativo.Value);

            if (planoId.HasValue)
                query = query.Where(a => a.PlanoId == planoId.Value);

            var resultado = query.ToList().Select(a => a.ToResponseDTO());

            return Ok(resultado);
        }

        // alunos
        [HttpPost]
        public ActionResult<AlunoResponseDTO> Post(AlunoRequestDTO dto)
        {
            var planoExiste = _context.Planos.Any(p => p.PlanoId == dto.PlanoId);
            if (!planoExiste)
            {
                return BadRequest($"Plano com ID {dto.PlanoId} não encontrado.");
            }

            var aluno = dto.ToModel();
            
            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            _context.Entry(aluno).Reference(a => a.Planos).Load();

            return CreatedAtRoute("ObterAluno", new { id = aluno.AlunoId }, aluno.ToResponseDTO());
        }

        // alunos/id
        [HttpPut("{id:int}")]
        public ActionResult<AlunoResponseDTO> Put(int id, AlunoRequestDTO dto)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.AlunoId == id);

            if (aluno is null)
            {
                return NotFound("Aluno não encontrado.");
            }

            var planoExiste = _context.Planos.Any(p => p.PlanoId == dto.PlanoId);
            if (!planoExiste)
            {
                return BadRequest($"Plano com ID {dto.PlanoId} não encontrado.");
            }

            aluno.Nome = dto.Nome;
            aluno.ImagemURL = dto.ImagemURL;
            aluno.Email = dto.Email;
            aluno.DataNascimento = dto.DataNascimento;
            aluno.Ativo = dto.Ativo;
            aluno.PlanoId = dto.PlanoId;

            _context.SaveChanges();
            _context.Entry(aluno).Reference(a => a.Planos).Load();

            return Ok(aluno.ToResponseDTO());
        }

        // alunos/id
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.AlunoId == id);

            if (aluno is null)
            {
                return NotFound("Aluno não localizado.");
            }

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();

            return Ok(new { mensagem = $"Aluno '{aluno.Nome}' removido com sucesso." });
        }
    }
}