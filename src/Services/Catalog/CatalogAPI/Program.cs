
using BuildingBlocs.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// here register service

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    //register all service into this project intpo mediator class library
    config.RegisterServicesFromAssembly(assembly);
    //register the pipline behavior 
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // executed first 
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();// using this option to perform thwe crud operations 


if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
          .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipline.
app.MapCarter(); // with this line when initalize the project will scan all ICarter and map the endpoints 
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
