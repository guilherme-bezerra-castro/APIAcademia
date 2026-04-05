using APIAcademia.Context;
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

        public ActionResult<Aluno> GetPrimeiro()
        {
            var aluno = _context.Alunos.FirstOrDefault();

            if (aluno is null)
            {
                return NotFound();
            }

            return aluno;
        }



        // /alunos

        [HttpGet]

        public ActionResult<IEnumerable<Aluno>> Get()
        {
            var alunos = _context.Alunos.AsNoTracking().ToList();

            if (alunos is null)
            {
                return NotFound();
            }

            return alunos;
        }



        // /alunos/id

        [HttpGet("{id:int}", Name = "ObterAluno")]

        public ActionResult<Aluno> Get(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.AlunoId == id);

            if (aluno is null)
            {
                return NotFound("Aluno não encontrado.");
            }

            return aluno;
        }

        [HttpPost]
        public ActionResult Post(CriarAlunoDTO dto)
        { 
            if (dto is null)
            {
                return BadRequest();
            }

            var aluno = new Aluno
            {
                Nome = dto.Nome,
                ImagemURL = dto.ImagemURL,
                Email = dto.Email,
                DataNascimento = dto.DataNascimento,
                Ativo = dto.Ativo,
                PlanoId = dto.PlanoId
            };

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterAluno", new { id = aluno.AlunoId }, aluno);
        }



        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CriarAlunoDTO dto)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.AlunoId == id);

            if (aluno is null)
            {
                return NotFound("Aluno não encontrado.");
            }

            aluno.Nome = dto.Nome;
            aluno.ImagemURL = dto.ImagemURL;
            aluno.Email = dto.Email;
            aluno.DataNascimento = dto.DataNascimento;
            aluno.Ativo = dto.Ativo;
            aluno.PlanoId = dto.PlanoId;

            _context.SaveChanges();

            return Ok(aluno);
        }

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

            return Ok(aluno);
        }
    }

    // Criação de Inline DTOs
    public record CriarAlunoDTO(string Nome, string ImagemURL, string Email, DateTime DataNascimento, bool Ativo, int PlanoId);
}