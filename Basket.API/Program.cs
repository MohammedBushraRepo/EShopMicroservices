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

//Register the custom exception handler 
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Configure the HTTP request pipeline.

var app = builder.Build();


app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();

