using Core.Messages;
using FluentValidation;

namespace API.Application.Commands.PessoaCommand
{
    public class RemoverEnderecoPessoaCommand : Command
    {
        public RemoverEnderecoPessoaCommand(int pessoaId, int enderecoId)
        {
            PessoaId = pessoaId;
            EnderecoId = enderecoId;
        }

        public int PessoaId { get; set; }
        public int EnderecoId { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new EnderecoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        public class EnderecoValidation : AbstractValidator<RemoverEnderecoPessoaCommand>
        {
            public EnderecoValidation()
            {
                RuleFor(c => c.PessoaId)
                    .GreaterThan(0)
                    .WithMessage("Informe o id da pessoa");

                RuleFor(c => c.EnderecoId)
                   .GreaterThan(0)
                   .WithMessage("Informe o id do endereço");
            }
        }
    }
}
