using BuildingBlock.Exceptions;

namespace Basket.API.Exeception
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string userName) : base("basket", userName)
        {
            
        }
    }
}
