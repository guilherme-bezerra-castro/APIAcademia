using APIAcademia.DTOs.Planos;
using FluentValidation;

namespace APIAcademia.Validators
{
    public class PlanoRequestValidator : AbstractValidator<PlanoRequestDTO>
    {
        public PlanoRequestValidator()
        {
            RuleFor(x => x.PlanoNome)
                .NotEmpty().WithMessage("O nome do plano não pode ser vazio.")
                .MaximumLength(40).WithMessage("Nome do plano: máximo 40 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição não pode ser vazia.")
                .MaximumLength(400).WithMessage("Descrição: máximo 400 caracteres.");

            RuleFor(x => x.Mensalidade)
                .GreaterThan(0).WithMessage("A mensalidade deve ser maior que zero.");

            RuleFor(x => x.ImagemURL)
                .NotEmpty()
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("ImagemURL deve ser uma URL válida.");
        }
    }
}
