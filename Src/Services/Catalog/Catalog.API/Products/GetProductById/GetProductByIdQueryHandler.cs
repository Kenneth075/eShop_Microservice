using BuildingBlock.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Model;
using Marten;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductQuery(Guid Id) : IQuery<GetProductResult>;
    public record GetProductResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            return product == null ? throw new ProductNotFoundException(request.Id) : new GetProductResult(product);
        }
    }
}
