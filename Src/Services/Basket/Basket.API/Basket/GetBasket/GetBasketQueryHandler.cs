using Basket.API.Model;
using BuildingBlock.CQRS;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            //Todo
            //Get basket from database using Repository pattern.

            return new GetBasketResult(new ShoppingCart("User"));
        }
    }
}
