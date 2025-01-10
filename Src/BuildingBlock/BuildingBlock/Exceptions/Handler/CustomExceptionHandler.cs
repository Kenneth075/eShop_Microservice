using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error message: {execptionMessage}, Time of occurrance: {time}",
                exception.Message, DateTime.UtcNow);

            (string Details, string Title, int StatusCode) = exception switch
            {
                InternalServerException =>
                (
                   exception.Message,
                   exception.GetType().Name,
                   httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                   exception.Message,
                   exception.GetType().Name,
                   httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException =>
                (
                   exception.Message,
                   exception.GetType().Name,
                   httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundException =>
                (
                   exception.Message,
                   exception.GetType().Name,
                   httpContext.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                 UnauthorizedAccessException=>
                (
                   exception.Message,
                   exception.GetType().Name,
                   httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized
                ),
                _ =>
                (
                   exception.Message,
                   exception.GetType().Name,
                   httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
                
            };

            var problemDetails = new ProblemDetails
            {
                Title = Title,
                Detail = Details,
                Status = StatusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("validationError", validationException.Errors);
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }
    }
}
