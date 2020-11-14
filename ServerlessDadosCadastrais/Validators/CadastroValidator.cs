using FluentValidation;
using ServerlessDadosCadastrais.Models;

namespace ServerlessDadosCadastrais.Validators
{
    public class CadastroValidator : AbstractValidator<Cadastro>
    {
        public CadastroValidator()
        {
            RuleFor(c => c.nome).NotEmpty().WithMessage("Preencha o campo 'nome'")
                .MinimumLength(5).WithMessage("O campo 'nome' deve possuir no mínimo 5 caracteres'");

            RuleFor(c => c.nome_pai).NotEmpty().WithMessage("Preencha o campo 'nome_pai'")
                .MinimumLength(5).WithMessage("O campo 'nome_pai' deve possuir no mínimo 5 caracteres'");

            RuleFor(c => c.nome_mae).NotEmpty().WithMessage("Preencha o campo 'nome_mae'")
                .MinimumLength(5).WithMessage("O campo 'nome_mae' deve possuir no mínimo 5 caracteres'");

            RuleFor(c => c.tecnologia).NotEmpty().WithMessage("Preencha o campo 'tecnologia'")
                .MinimumLength(1).WithMessage("O campo 'tecnologia' deve possuir no mínimo 1 caracter'");

            RuleFor(c => c.idade).NotEmpty().WithMessage("Preencha o campo 'idade'")
                .GreaterThan(0).WithMessage("O campo 'idade' deve ser maior do que 0'");

            RuleFor(c => c.cidade).NotEmpty().WithMessage("Preencha o campo 'cidade'")
                .MinimumLength(2).WithMessage("O campo 'cidade' deve possuir no mínimo 2 caracteres");

            RuleFor(c => c.aceito_novidades).NotEmpty().WithMessage("Preencha o campo 'aceito_novidades' com 'true' ou 'false'");
        }        
    }
}