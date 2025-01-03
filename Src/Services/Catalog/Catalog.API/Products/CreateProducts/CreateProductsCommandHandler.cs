using BuildingBlock.CQRS;
using Catalog.API.Model;
using Marten;

namespace Catalog.API.Products.CreateProducts
{
    public record CreateProductsCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductsResult>;

    public record CreateProductsResult(Guid Id);


    public class CreateProductsCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductsCommand, CreateProductsResult>
    {
        private readonly IDocumentSession session = session;

        public async Task<CreateProductsResult> Handle(CreateProductsCommand command, CancellationToken cancellationToken)
        {
            //Business logic to create a product
            //1.Create product entity from command object
            //2.Save to database
            //3.Return result

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //Save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //Return result
            return new CreateProductsResult(product.Id);
        }
    }
}
