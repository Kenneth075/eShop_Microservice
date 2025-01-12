using Basket.API.Model;
using BuildingBlock.CQRS;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = command.Cart;

            //TODO: saving basket to database.
            //TODO: Update Cache.

            return new StoreBasketResult("Username");
        }
    }
}
