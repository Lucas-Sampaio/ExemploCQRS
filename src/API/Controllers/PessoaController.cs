using API.Application.Commands.PessoaCommand;
using Core.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
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
            if (!response.IsValid) return BadRequest(response);
            return Created("", response);
        }

        [HttpPut("")]
        public async Task<IActionResult> Put(AtualizarPessoaCommand command)
        {
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) return BadRequest(response);
            return Created("", response);
        }

        [HttpPost("endereco")]
        public async Task<IActionResult> PostEndereco(AdicionarEnderecoPessoaCommand command)
        {
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) return BadRequest(response);
            return Created("", response);
        }

        [HttpPut("endereco")]
        public async Task<IActionResult> PutEndereco(AtualizarEnderecoPessoaCommand command)
        {
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) return BadRequest(response);
            return Created("", response);
        }

        [HttpDelete("{id}/endereco/{enderecoId}")]
        public async Task<IActionResult> DeleteEndereco(int id, int enderecoId)
        {
            var command = new RemoverEnderecoPessoaCommand(id, enderecoId);
            var response = await _mediator.EnviarComando(command);
            if (!response.IsValid) return BadRequest(response);
            return Created("", response);
        }

    }
}
