using ControleDeAcesso26.Exceptions.Base;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.Exceptions.ResponseError;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControleDeAcesso26.API.Filters
{
    public class ExceptionsFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ControleDeAcesso26Exception)
            {
                HandleControleDeAcesso26Exception(context);
            }
        }

        private static void HandleControleDeAcesso26Exception(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException exception)
            {
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages));
                context.ExceptionHandled = true;
            }
        }
    }
}
