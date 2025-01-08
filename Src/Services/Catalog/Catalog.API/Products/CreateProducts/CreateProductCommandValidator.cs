using FluentValidation;

namespace Catalog.API.Products.CreateProducts
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductsCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price most be greater than 0");
        }
    }
}
