namespace Basket.API.Basket.GetBasket;

public class GetBasketEndpoints : ICarterModule  // ❌ ICarterModule doesn't exist in global usings
{
    public void AddRoutes(IEndpointRouteBuilder app)  // ❌ Missing semicolon or wrong syntax
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            return Results.Ok(result);
        });
    }
}