using API.Application.Commands.PessoaCommand;
using Core.Communication.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class PessoaController : MainController
    {
        private readonly IMediatorHandler _mediator;

        public PessoaController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("")]
        public async Task<IActionResult> Post(AdicionarPessoaCommand command)
        {
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) AdicionarErroProcessamento(response);
            return CustomResponse("Pessoa criada com sucesso", StatusCodes.Status201Created);
        }

        [HttpPut("")]
        public async Task<IActionResult> Put(AtualizarPessoaCommand command)
        {
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) AdicionarErroProcessamento(response);
            return CustomResponse("Pessoa atualizada com sucesso");
        }

        [HttpPost("endereco")]
        public async Task<IActionResult> PostEndereco(AdicionarEnderecoPessoaCommand command)
        {
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) AdicionarErroProcessamento(response);
            return CustomResponse("Endereco criado com sucesso", StatusCodes.Status201Created);
        }

        [HttpPut("endereco")]
        public async Task<IActionResult> PutEndereco(AtualizarEnderecoPessoaCommand command)
        {
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) AdicionarErroProcessamento(response);
            return CustomResponse("Endereco atualizado com sucesso");
        }

        [HttpDelete("{id}/endereco/{enderecoId}")]
        public async Task<IActionResult> DeleteEndereco(int id, int enderecoId)
        {
            var command = new RemoverEnderecoPessoaCommand(id, enderecoId);
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) AdicionarErroProcessamento(response);
            return CustomResponse("Endereco removido com sucesso");
        }

    }
}
