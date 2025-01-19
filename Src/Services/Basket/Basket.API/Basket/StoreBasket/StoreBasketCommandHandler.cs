using Basket.API.Data;
using Basket.API.Model;
using BuildingBlock.CQRS;
using Discount.Grpc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.Cart, cancellationToken);

            await repository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }

        public async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            //Communicate with the Discount.gRPC and calculate the lastest product price.
            foreach (var items in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = items.ProductName }, cancellationToken: cancellationToken);
                items.Price -= coupon.Amount;
            }

        }
    }
}
