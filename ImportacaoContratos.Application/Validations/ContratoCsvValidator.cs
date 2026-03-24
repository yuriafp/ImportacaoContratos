using FluentValidation;
using ImportacaoContratos.Application.DTOs;
using ImportacaoContratos.Application.Extensions;

namespace ImportacaoContratos.Application.Validations;

public class ContratoCsvValidator : AbstractValidator<ContratoCsvRecord>
{
    public ContratoCsvValidator()
    {
        // Regras para o Nome
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

        // Valida Cpf
        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(cpf => !string.IsNullOrWhiteSpace(cpf) && cpf.SomenteNumeros().Length == 11)
            .WithMessage("O formato do CPF é inválido ou não tem 11 dígitos.");

        // Exigi número Contrato
        RuleFor(x => x.Contrato)
            .NotEmpty().WithMessage("O número do contrato é obrigatório.");

        // Exigi Produto
        RuleFor(x => x.Produto)
            .NotEmpty().WithMessage("O produto financeiro é obrigatório.");

        // Nada menor que 0 
        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("O valor do contrato deve ser maior que zero.");
      
    }
}