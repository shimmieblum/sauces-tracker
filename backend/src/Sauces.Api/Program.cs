using Microsoft.EntityFrameworkCore;
using Sauces.Api.Handlers;
using Sauces.Api.Repositories;
using Sauces.Core;
using Serilog;

var builder = WebApplication
    .CreateBuilder(args);

var configManager = builder.Configuration;

builder.Services
    .AddLogging(loggerBuilder => loggerBuilder.AddSerilog(
        new LoggerConfiguration().WriteTo.Console().CreateLogger()))
    .AddTransient<ISaucesRepository, SauceRepository>()
    .AddTransient<IFermentationRepository, FermentationRepository>()
    .AddDbContext<SaucesContext>( 
        opts => opts.UseNpgsql(configManager.GetConnectionString("SaucesDB")), 
        ServiceLifetime.Transient);

var app = builder.Build();
app.MapGet("", () => "welcome to the sauces stuff!!!");

app.MapGroup("/fermentations").MapFermentationApi();
app.MapGroup("/sauces").MapSauceApi();

app.Run();
