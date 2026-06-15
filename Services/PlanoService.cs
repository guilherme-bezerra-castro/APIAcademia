using APIAcademia.DTOs.Planos;
using APIAcademia.Extensions;
using APIAcademia.Repositories;

namespace APIAcademia.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoRepository _planoRepository;

        public PlanoService(IPlanoRepository planoRepository)
        {
            _planoRepository = planoRepository;
        }

        public async Task<PlanoResponseDTO?> AtualizarAsync(int id, PlanoRequestDTO dto)
        {
            var plano = await _planoRepository.ObterPorIdAsync(id);
            if (plano is null) 
                return null;

            plano.PlanoNome = dto.PlanoNome;
            plano.ImagemURL = dto.ImagemURL;
            plano.Descricao = dto.Descricao;
            plano.Mensalidade = dto.Mensalidade;

            await _planoRepository.AtualizarAsync(plano);
            await _planoRepository.SalvarAsync();

            return plano.ToResponseDTO();
        }

        public async Task<PlanoResponseDTO> CriarAsync(PlanoRequestDTO dto)
        {
            var plano = dto.ToModel();

            await _planoRepository.CriarAsync(plano);
            await _planoRepository.SalvarAsync();

            return plano.ToResponseDTO();
        }

        public async Task<PlanoResponseDTO?> ObterPorIdAsync(int id)
        {
            var plano = await _planoRepository.ObterPorIdComAlunosAsync(id);

            return plano?.ToResponseDTO();
        }

        public async Task<IEnumerable<PlanoResponseDTO>> ObterTodosAsync()
        {
            var planos = await _planoRepository.ObterTodosComAlunosAsync();
        
            return planos.Select(p => p.ToResponseDTO());
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var plano = await _planoRepository.ObterPorIdAsync(id);
            if (plano is null) 
                return false;

            await _planoRepository.RemoverAsync(plano);
            await _planoRepository.SalvarAsync();
            
            return true;
        }
    }
}
