using Core.Messages;
using FluentValidation;

namespace API.Application.Commands.PessoaCommand
{
    public class AdicionarEnderecoPessoaCommand : Command
    {
        public int PessoaId { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new EnderecoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class EnderecoValidation : AbstractValidator<AdicionarEnderecoPessoaCommand>
        {
            public EnderecoValidation()
            {
                RuleFor(c => c.PessoaId)
                    .GreaterThan(0)
                    .WithMessage("Informe o id da pessoa");

                RuleFor(c => c.Logradouro)
                    .NotEmpty()
                    .WithMessage("Informe o Logradouro")
                    .MaximumLength(250)
                    .WithMessage("Logradouro só pode no máximo ate 250 caracteres");

                RuleFor(c => c.Numero)
                    .NotEmpty()
                    .WithMessage("Informe o Número");

                RuleFor(c => c.CEP)
                    .NotEmpty()
                    .WithMessage("Informe o CEP");

                RuleFor(c => c.Bairro)
                    .NotEmpty()
                    .WithMessage("Informe o Bairro");

                RuleFor(c => c.Cidade)
                    .NotEmpty()
                    .WithMessage("Informe o Cidade");

                RuleFor(c => c.Estado)
                    .NotEmpty()
                    .WithMessage("Informe o Estado");
            }
        }
    }
}
