using ContactsManager.Core.Builders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddServices(builder.Configuration);

// Configure middleware
WebApplication app = builder.Build();
app.AddMiddleware();

// Run API
app.Run();