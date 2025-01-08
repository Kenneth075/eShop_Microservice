﻿using BuildingBlock.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Model;
using Marten;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductQuery(Guid Id) : IQuery<GetProductResult>;
    public record GetProductResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler.Handler called {@Query}", request);
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            return product == null ? throw new ProductNotFoundException() : new GetProductResult(product);
        }
    }
}
