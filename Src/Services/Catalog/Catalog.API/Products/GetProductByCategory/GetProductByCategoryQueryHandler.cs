using BuildingBlock.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Model;
using Marten;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategory(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategory, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategory query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync();
            
            return products == null ? null! : new GetProductByCategoryResult(products);
        }
    }
}
