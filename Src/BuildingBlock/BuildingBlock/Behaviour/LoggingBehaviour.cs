using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlock.Behaviour
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[StART], Handle request = {Request} - Response = {Response} - RequestData = {RequestData}", 
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var time = new Stopwatch();
             time.Start();

            var response = await next();

            time.Stop();

            var timeTaken = time.Elapsed;
            if (timeTaken.Seconds > 3) 
                logger.LogInformation("[PERFORMANCE], The request {request} took {timeTake}", typeof(TRequest).Name, timeTaken);

            logger.LogInformation("[END], Handle Request {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
