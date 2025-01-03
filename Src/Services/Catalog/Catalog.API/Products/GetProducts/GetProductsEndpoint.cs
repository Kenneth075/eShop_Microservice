using Carter;
using Catalog.API.Model;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async (ISender send) =>
            {
                var result = await send.Send(new GetProductQuery());

                var response = result.Adapt<GetProductResult>();

                return Results.Ok(response);
            }).WithName("GetProducts")
            .Produces<GetProductResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get all products")
            .WithDescription("Get all products");

        }
    }
}
