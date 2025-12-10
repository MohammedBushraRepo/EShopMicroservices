

namespace CatalogAPI.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    //create a command and map the incoming request to the command 
                    var command = request.Adapt<CreateProductCommand>();

                    //trigger the mediator handler class

                    var result = await sender.Send(command);


                    //map the returned response and map it back 

                    var response = result.Adapt<CreateProductResponse>();

                    return Results.Created($"/products/{response.Id}", response);
                })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
