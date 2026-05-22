using APIAcademia.Context;
using APIAcademia.DTOs.Planos;
using APIAcademia.Extensions;
using APIAcademia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlanosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanosController(AppDbContext context)
        {
            _context = context;
        }

        // planos/alunos
        [HttpGet("Alunos")]
        public ActionResult<IEnumerable<PlanoResponseDTO>> GetPlanosAlunos()
        {
            var planos = _context.Planos.Include(p => p.Alunos).AsNoTracking().ToList();

            return Ok(planos.Select(p => p.ToResponseDTO()));
        }

        // planos
        [HttpGet]
        public ActionResult<IEnumerable<PlanoResponseDTO>> Get()
        {
            var planos = _context.Planos.Include(p => p.Alunos).AsNoTracking().ToList();

            return Ok(planos.Select(p => p.ToResponseDTO()));
        }

        [HttpGet("{id:int}", Name = "ObterPlano")]
        public ActionResult<PlanoResponseDTO> Get(int id)
        {
            var plano = _context.Planos.Include(p => p.Alunos).AsNoTracking().FirstOrDefault(p => p.PlanoId == id);

            if (plano is null)
            {
                return NotFound($"Plano com ID {id} não encontrado.");
            }

            return Ok(plano.ToResponseDTO());
        }

        // planos
        [HttpPost]
        public ActionResult<PlanoResponseDTO> Post(PlanoRequestDTO dto)
        {
            var plano = dto.ToModel();

            _context.Planos.Add(plano);
            _context.SaveChanges();

            return CreatedAtRoute("ObterPlano", new { id = plano.PlanoId }, plano.ToResponseDTO());
        }

        // planos/id
        [HttpPut("{id:int}")]
        public ActionResult<PlanoResponseDTO> Put(int id, PlanoRequestDTO dto)
        {
            var plano = _context.Planos.FirstOrDefault(p => p.PlanoId == id);
            if (plano is null)
            {
                return NotFound("Plano não localizado.");
            }

            plano.PlanoNome = dto.PlanoNome;
            plano.ImagemURL = dto.ImagemURL;
            plano.Descricao = dto.Descricao;
            plano.Mensalidade = dto.Mensalidade;

            _context.SaveChanges();

            return Ok(plano.ToResponseDTO());
        }

        // planos/id
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var plano = _context.Planos.FirstOrDefault(p => p.PlanoId == id);

            if (plano is null)
            {
                return NotFound("Plano não localizado.");
            }

            _context.Planos.Remove(plano);
            _context.SaveChanges();

            return Ok(new { mensagem = $"Plano '{plano.PlanoNome}' removido com sucesso." });
        }
    }
}
