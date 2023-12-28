using Sauces.Api.Handlers;
using Sauces.Api.Repositories;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services
    .AddSingleton<ISaucesRepository, SauceRepository>();

var app = builder.Build();

app.MapGroup("/fermentations").MapFermentationApi();
app.MapGroup("/sauces").MapSauceApi();

app.Run();

