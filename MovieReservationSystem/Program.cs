using MovieReservationSystem.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Host.ConfigureLogging();

// Configure services
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureCors();

var app = builder.Build();

// Configure middlewares
app.ConfigureMiddlewares(app.Environment);

app.Run();