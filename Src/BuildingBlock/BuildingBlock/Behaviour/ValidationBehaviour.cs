using BuildingBlock.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlock.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>>validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failure = validationResults.Where(r => r.Errors.Count > 0).SelectMany(r=>r.Errors).ToList();

            if (failure.Count > 0)
                throw new ValidationException(failure);

            return await next();
        }
    }
}
