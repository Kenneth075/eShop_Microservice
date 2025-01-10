using Catalog.API.Model;
using Marten;
using Marten.Internal.Sessions;
using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitiaData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            //Marten UPSERT will cater for existing record
            session.Store<Product>(PreConfigureProduct());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> PreConfigureProduct() => new List<Product>()
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Books",
                Category = new List<string> {"NoteBooks, TextBooks" },
                Description = "Study materials",
                ImageFile = "Book.jpng",
                Price = 1000.00M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mobile Phones",
                Category = new List<string> {"Iphone, Samsung" },
                Description = "Mobile gadget",
                ImageFile = "Mobile.jpng",
                Price = 3000.00M
            }

        }; 
       
    }
}
