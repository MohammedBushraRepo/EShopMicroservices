using Basket.API.Models;
using BuildingBlocs.CQRS;
using Marten.Linq.QueryHandlers;

namespace Basket.API.Basket.GetBasket
{

    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
 

        public  async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            //get basket from database 
            // var basket = await _repository.GetBasket(request.username);

            return new GetBasketResult(new ShoppingCart("swn"));
        }
    }
}
