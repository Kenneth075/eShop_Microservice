using Carter;
using Catalog.API.Model;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender send) =>
            {
                var result = await send.Send(new GetProductQuery(id));

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);

            }).WithName("GetProductById")
            .Produces<GetProductResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get product")
            .WithDescription("Get product");

        }
    }
}
