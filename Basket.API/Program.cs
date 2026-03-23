using BuildingBlocks.Behaviors;
using BuildingBlocs.Behaviors;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    //opts.AutoCreateSchemaObjects
}).UseLightweightSessions();

//DI registeration 
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//We will use Scrutor lib to Allow us register two dependancies from same abstraction 
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

//Register Redis for istributed Caching 
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.Instance = "Basket";
});
//Register the custom exception handler 
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//register Health checks 
builder.Services.AddHealthChecks();



// Configure the HTTP request pipeline.

var app = builder.Build();


app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/heath");
app.Run();

