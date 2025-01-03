using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.CreateProducts
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule 
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Products", async (CreateProductRequest request, ISender send) =>
            {
                var command = request.Adapt<CreateProductsCommand>();

                var result = await send.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/Products/{response.Id}", response);

            }).WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        
        }
    }
}
