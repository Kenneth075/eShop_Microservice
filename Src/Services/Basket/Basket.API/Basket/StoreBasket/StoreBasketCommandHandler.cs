using Basket.API.Data;
using Basket.API.Model;
using BuildingBlock.CQRS;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = command.Cart;

            await repository.StoreBasket(shoppingCart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }
    }
}
