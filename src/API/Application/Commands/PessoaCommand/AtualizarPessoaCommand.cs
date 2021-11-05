using Core.Messages;
using Domain.PessoaAggregate;
using FluentValidation;
using System;

namespace API.Application.Commands.PessoaCommand
{
    public class AtualizarPessoaCommand : Command
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarPessoaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        public class AtualizarPessoaValidation : AbstractValidator<AtualizarPessoaCommand>
        {
            public AtualizarPessoaValidation()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .WithMessage("Informe o id da pessoa");

                RuleFor(x => x.Nome)
                    .MinimumLength(2).WithMessage("O nome precisa ter pelo menos 2 caracteres")
                    .MaximumLength(150).WithMessage("O nome pode ter no máximo 150 caracteres");

                RuleFor(x => x.DataNascimento)
                    .GreaterThan(DateTime.MinValue)
                    .LessThan(DateTime.Today)
                    .WithMessage("Informe uma data de nascimento valida");

                RuleFor(x => x.Cpf)
                    .Must(TerCpfValido)
                    .WithMessage("O cpf informado não é valido");

            }
            protected static bool TerCpfValido(string cpf)
            {
                return CPF.Validar(cpf);
            }
        }
    }
}
