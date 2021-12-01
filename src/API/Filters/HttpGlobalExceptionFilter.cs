using Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace API.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);


            if (context.Exception.GetType() == typeof(DomainException))
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Consulte a propriedade de erros para obter detalhes adicionais."
                };

                problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "Ocorreu um erro inesperado tente novamente." }
                };

                if (env.IsDevelopment())
                {
                    json.DeveloperMessage = new DeveloperErrorMessage
                    {
                        Message = context.Exception.Message,
                        Stacktrace = context.Exception.StackTrace,
                        InnerExceptionMessage = context.Exception.InnerException?.Message
                    };
                }


                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public DeveloperErrorMessage DeveloperMessage { get; set; }
        }
        private class DeveloperErrorMessage
        {
            public string Message { get; set; }
            public string Stacktrace { get; set; }
            public string InnerExceptionMessage { get; set; }
        }
    }
}
