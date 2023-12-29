using Microsoft.EntityFrameworkCore;
using Sauces.Api.Handlers;
using Sauces.Api.Repositories;
using Sauces.Core;

var builder = WebApplication
    .CreateBuilder(args);

var configManager = builder.Configuration;
builder.Services
    .AddTransient<ISaucesRepository, SauceRepository>()
    .AddTransient<IFermentationRecipeRepository, FermentationRecipeRepository>()
    .AddDbContext<SaucesContext>( 
        opts => opts.UseNpgsql(configManager.GetConnectionString("SaucesDB")), 
        ServiceLifetime.Transient);

var app = builder.Build();
app.MapGet("", () => "hello world!");

app.MapGroup("/fermentations").MapFermentationApi();
app.MapGroup("/sauces").MapSauceApi();

app.Run();
return;

void ConfigureServices(IServiceCollection services, ConfigurationManager configManager)
{
    services.AddDbContext<SaucesContext>(
        opts =>
        {
            opts.UseNpgsql(configManager.GetConnectionString("SaucesDB"));
        }, ServiceLifetime.Transient);
}