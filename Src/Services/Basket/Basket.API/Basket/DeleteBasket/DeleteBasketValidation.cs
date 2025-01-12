using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{
    public class DeleteBasketValidation : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        }
    }
}
