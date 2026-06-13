using APIAcademia.DTOs;
using APIAcademia.DTOs.Alunos;
using APIAcademia.Extensions;
using APIAcademia.Repositories;

namespace APIAcademia.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<AlunoResponseDTO?> AtualizarAsync(int id, AlunoRequestDTO dto)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno is null) return null;

            aluno.Nome = dto.Nome;
            aluno.ImagemURL = dto.ImagemURL;
            aluno.Email = dto.Email;
            aluno.DataNascimento = dto.DataNascimento;
            aluno.Ativo = dto.Ativo;
            aluno.PlanoId = dto.PlanoId;

            await _alunoRepository.AtualizarAsync(aluno);
            await _alunoRepository.SalvarAsync();

            var atualizado = await _alunoRepository.ObterPorIdComPlanoAsync(id);
            return atualizado?.ToResponseDTO();
        }

        public async Task<AlunoResponseDTO?> AtualizarStatusAsync(int id, bool ativo)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno is null) return null;

            aluno.Ativo = ativo;
            await _alunoRepository.AtualizarAsync(aluno);
            await _alunoRepository.SalvarAsync();

            var atualizado = await _alunoRepository.ObterPorIdComPlanoAsync(id);
            return atualizado?.ToResponseDTO();
        }

        public async Task<AlunoResponseDTO> CriarAsync(AlunoRequestDTO dto)
        {
            var aluno = dto.ToModel();
            await _alunoRepository.CriarAsync(aluno);
            await _alunoRepository.SalvarAsync();

            var criado = await _alunoRepository.ObterPorIdComPlanoAsync(aluno.AlunoId);
            return criado!.ToResponseDTO();
        }

        public async Task<IEnumerable<AlunoResponseDTO>> FiltrarAsync(bool? ativo, int? planoId)
        {
            var alunos = await _alunoRepository.FiltrarAsync(ativo, planoId);
            return alunos.Select(a => a.ToResponseDTO());
        }

        public async Task<ResultadoPaginado<AlunoResponseDTO>> ObterPaginadoAsync(int pagina, int itensPorPagina)
        {
            var (itens, total) = await _alunoRepository
                .ObterPaginadoAsync(pagina, itensPorPagina);

            return new ResultadoPaginado<AlunoResponseDTO>
            {
                TotalItens = total,
                TotalPaginas = (int)Math.Ceiling(total / (double)itensPorPagina),
                PaginaAtual = pagina,
                ItensPorPagina = itensPorPagina,
                Dados = itens.Select(a => a.ToResponseDTO())
            };
        }

        public async Task<AlunoResponseDTO?> ObterPorIdAsync(int id)
        {
            var aluno = await _alunoRepository.ObterPorIdComPlanoAsync(id);
            return aluno?.ToResponseDTO();
        }

        public async Task<AlunoResponseDTO?> ObterPrimeiroAsync()
        {
            var alunos = await _alunoRepository.ObterTodosComPlanoAsync();
            var primeiro = alunos.FirstOrDefault();
            return primeiro?.ToResponseDTO();
        }

        public async Task<IEnumerable<AlunoResponseDTO>> ObterTodosAsync()
        {
            var alunos = await _alunoRepository.ObterTodosComPlanoAsync();
            return alunos.Select(a => a.ToResponseDTO());
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno is null) return false;

            await _alunoRepository.RemoverAsync(aluno);
            await _alunoRepository.SalvarAsync();
            return true;
        }
    }
}
