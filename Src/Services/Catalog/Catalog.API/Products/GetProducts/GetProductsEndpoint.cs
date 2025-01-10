using Carter;
using Catalog.API.Model;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductRequest(int? PageNumber = 1, int? PageSize=10);
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async ([AsParameters] GetProductRequest request, ISender send) =>
            {
                var quary = request.Adapt<GetProductQuery>();

                var result = await send.Send(quary);

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
