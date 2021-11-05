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
        public async Task<IActionResult> Post(AdicionarPessoaCommand pessoaCommand)
        {
            var response = await _mediator.EnviarComando(pessoaCommand);
            if (!response.IsValid) return BadRequest(response);
            return Created("", response);
        }

        [HttpPut("")]
        public async Task<IActionResult> Put(AtualizarPessoaCommand pessoaCommand)
        {
            var response = await _mediator.EnviarComando(pessoaCommand);
            if (!response.IsValid) return BadRequest(response);
            return Created("", response);
        }
    }
}
