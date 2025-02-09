using Ordering.Domain.Model;
using Ordering.Domain.ValueObject;

namespace Ordering.Infrastructure.Data.DatabaseExtension
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("d0321ed0-3afd-425f-9ddf-7e2695992543")),"Kenny","keny@gmail.com"),
                //Customer.Create(CustomerId.Of(new Guid("671a130a-bec3-4855-b9ae-a3dcb4c053a3")),"Ken","ken@gmail.com"),
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("3312ebef-456c-40b4-b456-420e7050a537")), "Samsung", 5000),
                //Product.Create(ProductId.Of(new Guid("0481a1ad-5530-48e2-8652-4294cd7efd76")), "Iphone", 6000)
            };

        public static IEnumerable<Order> OrderWithItems
        {
            get
            {
                var address = Address.Of("Kenny", "Joe", "sam@gmail.com", "Lagos Island", "Nigeria", "Lagos", "100001");
                var payment = Payment.Of("Kenny Joe", "554593438595839", "054", "10/11/2025", 1);

                var order = Order.Create(OrderId.Of(new Guid("4ec40d76-a58d-4887-a5f1-6f03766c66f9")), CustomerId.Of(
                    new Guid("424903d9-10b5-435f-8874-767e4c9118fc")), OrderName.Of("Ord_1"),
                    billingAddress: address,
                    shippingAddress: address,
                    payment);

                order.Add(ProductId.Of(new Guid("3312ebef-456c-40b4-b456-420e7050a537")), 2, 10000);

                return new List<Order> { order };
            }
        }
            
    }
}
