using ControleDeAcesso26.Exceptions.Base;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.Exceptions.ResponseError;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
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
            else
            {
                HandleControleDeAcesso26UnknownException(context);
            }
        }

        private static void HandleControleDeAcesso26Exception(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException errorOnValidationException)
            {
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(errorOnValidationException.ErrorMessages));
                context.ExceptionHandled = true;
            }

            if (context.Exception is NotFoundException notFoundException)
            {
                context.Result = new NotFoundObjectResult(new ResponseErrorJson(notFoundException.ErrorMessage));
            }
        }

        private static void HandleControleDeAcesso26UnknownException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ValidatorsRulesResourceMessages.ERRO_DESCONHECIDO));
        }
    }
}
