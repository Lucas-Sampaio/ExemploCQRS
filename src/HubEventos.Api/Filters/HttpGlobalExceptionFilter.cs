using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace HubEventos.Api.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
