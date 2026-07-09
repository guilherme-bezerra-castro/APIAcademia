using APIAcademia.DTOs.Alunos;
using FluentValidation;

namespace APIAcademia.Validators
{
    public class AlunoRequestValidator : AbstractValidator<AlunoRequestDTO>
    {
        public AlunoRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome não pode ser vazio.")
                .MaximumLength(80).WithMessage("Nome: máximo 80 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail não pode ser vazio.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.")
                .MaximumLength(100).WithMessage("E-mail: máximo 100 caracteres.");

            RuleFor(x => x.ImagemURL)
                .NotEmpty().WithMessage("A URL da imagem não pode ser vazia.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("ImagemURL deve ser uma URL válida (ex: https://...)");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.Today)
                .WithMessage("A data deve ser no passado.")
                .GreaterThan(DateTime.Today.AddYears(-120))
                .WithMessage("Data de nascimento inválida.");

            RuleFor(x => x.PlanoId)
                .GreaterThan(0).WithMessage("PlanoId deve ser um valor positivo.");
        }
    }
}
