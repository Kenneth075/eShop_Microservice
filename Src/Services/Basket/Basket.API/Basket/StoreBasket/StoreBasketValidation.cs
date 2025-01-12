using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketValidation : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidation()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Shopping cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotNull().WithMessage("Username is required");
        }
    }
}
