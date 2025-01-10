using BuildingBlock.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Model;
using Marten;

namespace Catalog.API.Products.Updateproduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) :
        ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler>logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler. Handle called {reques}", request);

            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
                 throw new ProductNotFoundException(request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Category = request.Category;
            product.ImageFile = request.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
