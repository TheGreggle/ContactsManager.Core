using ContactsManager.Core.Database;

namespace ContactsManager.Core.Builders;

public static class Middleware
{
    /// <summary>
    /// Adds middleware to the request pipeline
    /// </summary>
    /// <param name="app">app</param>
    public static void AddMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts Manager API"));

            // Ensure DB is created and seeded
            using IServiceScope serviceScope = app.Services.CreateScope();
            ContactDbContext contactDbContext = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>();
            contactDbContext.Database.EnsureCreated();

            // Allow any host for Dev
            app.UseCors("AllowLocalDev");

        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}