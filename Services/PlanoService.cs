using APIAcademia.Context;
using APIAcademia.DTOs.Planos;
using APIAcademia.Extensions;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly AppDbContext _context;

        public PlanoService(AppDbContext context)
        {
            _context = context;
        }

        public PlanoResponseDTO? Atualizar(int id, PlanoRequestDTO dto)
        {
            var plano = _context.Planos.FirstOrDefault(p => p.PlanoId == id);
            if (plano is null) return null;

            plano.PlanoNome = dto.PlanoNome;
            plano.ImagemURL = dto.ImagemURL;
            plano.Descricao = dto.Descricao;
            plano.Mensalidade = dto.Mensalidade;

            _context.SaveChanges();
            return plano.ToResponseDTO();
        }

        public PlanoResponseDTO Criar(PlanoRequestDTO dto)
        {
            var plano = dto.ToModel();
            _context.Planos.Add(plano);
            _context.SaveChanges();

            return plano.ToResponseDTO();
        }

        public PlanoResponseDTO? ObterPorId(int id)
        {
            var plano = _context.Planos.Include(p => p.Alunos).AsNoTracking().FirstOrDefault(p => p.PlanoId == id);

            return plano?.ToResponseDTO();
        }

        public IEnumerable<PlanoResponseDTO> ObterTodos()
        {
            return _context.Planos.Include(p => p.Alunos).AsNoTracking().ToList().Select(p => p.ToResponseDTO());
        }

        public bool Remover(int id)
        {
            var plano = _context.Planos.FirstOrDefault(p => p.PlanoId == id);
            if (plano is null) return false;

            _context.Planos.Remove(plano);
            _context.SaveChanges();
            return true;
        }
    }
}
