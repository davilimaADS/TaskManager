using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Communication.Response;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Api.Filters
{
    public class ExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
           if (context.Exception is TaskManagerException)
            {
                HandleException(context);
            }
            else
            {
                ThrowUnkowError(context);
            }
        }

        private void HandleException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidateException ex)
            {
                var errorMessage = new ResponseErrorJson(ex.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorMessage);
            }
            else if (context.Exception is UserNotFoundException notFoundEx)
            {
                var errorMessage = new ResponseErrorJson(notFoundEx.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Result = new NotFoundObjectResult(errorMessage);
            }
            else if (context.Exception is TaskNotFoundException taskNotFoundEx)  // <- nova exception
            {
                var errorMessage = new ResponseErrorJson(taskNotFoundEx.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Result = new NotFoundObjectResult(errorMessage);
            }
            else if (context.Exception is ProjectNotFoundException projectNotFoundEx) // <-- aqui
            {
                var errorMessage = new ResponseErrorJson(projectNotFoundEx.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Result = new NotFoundObjectResult(errorMessage);
            }
            else
            {
                ThrowUnkowError(context);
            }
        }

        private void ThrowUnkowError(ExceptionContext context)
        {
            var errorMessage = new ResponseErrorJson("Unknown error");

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new ObjectResult(errorMessage);
        }
    }
}
