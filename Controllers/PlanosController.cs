using APIAcademia.Context;
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

        [HttpGet("Alunos")]
        public ActionResult<IEnumerable<Plano>> GetPlanosAlunos()
        {
            return _context.Planos.Include(a => a.Alunos).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Plano>> Get()
        {
            try
            {
                var planos = _context.Planos.ToList();
                return Ok(planos);
            }
            catch (Exception)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterPlano")]
        public ActionResult<Plano> Get(int id)
        {
            try
            {
                var plano = _context.Planos.FirstOrDefault(a => a.PlanoId == id);
                if (plano is null)
                {
                    return NotFound($"Plano com ID = {id} não encontrado.");
                }
                return Ok(plano);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult Post(CriarPlanoDTO dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            var plano = new Plano
            {
                PlanoNome = dto.PlanoNome,
                ImagemURL = dto.ImagemURL,
                Descricao = dto.Descricao,
                Mensalidade = dto.Mensalidade
            };

            _context.Planos.Add(plano);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterPlano", new { id = plano.PlanoId }, plano);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CriarPlanoDTO dto)
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

            return Ok(plano);
        }

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

            return Ok(plano);
        }
    }

    public record CriarPlanoDTO(string PlanoNome, string ImagemURL, string Descricao, decimal Mensalidade);

}
