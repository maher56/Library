using Library.Api.Extensions;
using Library.Api.Middleware;
using Library.Application.Services.Token;
using Library.Infrastructure;
using Library.Infrastructure.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(s =>
{
    s.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});
builder.Services.AddLocalization();
builder.Services.AddRequestLocalization(x =>
{
    x.DefaultRequestCulture = new RequestCulture("en");
    x.ApplyCurrentCultureToResponseHeaders = true;
    x.SupportedCultures = new List<CultureInfo> { new("en") };
    x.SupportedUICultures = new List<CultureInfo> { new("en") };
});
builder.Services.AddTransient<ITokenService, TokenService>()
                .AddSwaggerService()
                .AddJwtService(builder.Configuration["JWT:Key"])
                .AddInfrastructureServices(connectionString)
                .AddTransient<HandleExceptionMiddleware>();

builder.Services.AddSwaggerGen();

var app = builder.Build();
//seed
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    await Seeder.SeedAdmin(services);
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");
app.UseRequestLocalization();
app.UseMiddleware<HandleExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
