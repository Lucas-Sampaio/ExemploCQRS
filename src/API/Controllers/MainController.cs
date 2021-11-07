using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ICollection<string> Erros = new List<string>();

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }
        protected void AdicionarErroProcessamento(ValidationResult validationsResult)
        {
            foreach (var item in validationsResult.Errors)
            {
                AdicionarErroProcessamento(item.ErrorMessage);
            }
        }
        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }
        /// <summary>
        /// Retorna uma reposta de sucesso caso seja valido ou um bad request se conter erros
        /// </summary>
        /// <param name="result">Resposta que será enviada caso exista</param>
        /// <param name="successStatusCode">retorna algum codigo de sucesso</param>
        /// <returns></returns>
        protected ActionResult CustomResponse(object result = null, int successStatusCode = 0)
        {
            if (OperacaoValida())
            {
                switch (successStatusCode)
                {
                    case StatusCodes.Status201Created:
                        return Created("", result);
                    case StatusCodes.Status204NoContent:
                        return NoContent();
                    default:
                        return Ok(result);
                }

            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens", Erros.ToArray()}
            }));
        }
    }
}
