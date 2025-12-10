var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    //register all service into this project intpo mediator class library
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});


var app = builder.Build();

// Configure the HTTP request pipline.
app.MapCarter(); // with this line when initalize the project will scan all ICarter and map the endpoints 

app.Run();
