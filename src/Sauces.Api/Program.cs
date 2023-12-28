using System.Net.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sauces.Api;
using Sauces.Api.Models;
using Sauces.Core;
using Sauces.Core.Model;
using static Microsoft.AspNetCore.Http.Results;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapPost("/fermentation", ([FromBody] FermentationRecipeRequest fermentationRecipeRequest) =>
{
    using var db = new SaucesContext();
    var id = Guid.NewGuid();
    var fermentation = new FermentationRecipe
    {
        Id = id,
        Ingredients = fermentationRecipeRequest.Ingredients.Select(kv =>
        {
            var (ingredientName, percentage) = kv;
            var ingredient = db.Ingredients.Find(ingredientName)
                             ?? db.Ingredients.Add(new() { Name = ingredientName }).Entity;
            return new RecipeIngredient
            {
                Ingredient = ingredient, Percentage = percentage
            };
        }).ToList(),
        LengthInDays = fermentationRecipeRequest.LengthInDays
    };
    db.Add(fermentation);
    db.SaveChanges();

    return Ok(id);
});

app.MapPost("/sauce", ([FromBody]SauceRequest sauceRequest) =>
{
    using var db = new SaucesContext();
    Console.WriteLine($"Database path: {db.DbPath}");
    Console.WriteLine("adding new Sauce");
    var id = Guid.NewGuid();
    var fermentation = db.FermentationRecipes.Find(sauceRequest.Recipe.Fermentation);
    if (fermentation is null)
        return NotFound($"No Fermentation with id {sauceRequest.Recipe.Fermentation} found");
    var sauceRecipe = new SauceRecipe
    {
        Id = id,
        Fermentations =
        [
            new()
            {
                FermentationRecipe = fermentation,
                Percentage = sauceRequest.Recipe.FermentationPercentage
            }
        ],
        NonFermentedIngredients = sauceRequest.Recipe.NonFermentedIngredients?.Select(kv =>
        {
            var (ingredientName, percentage) = kv;
            var ingredient = db.Ingredients.Find(ingredientName) 
                             ?? db.Ingredients.Add(new Ingredient { Name = ingredientName }).Entity;
            return new RecipeIngredient { Id = Guid.NewGuid(), Ingredient = ingredient, Percentage = percentage };
        }).ToList() ?? []
    };
    var sauce = new Sauce
    {
        Id = Guid.NewGuid(),
        Name = sauceRequest.Name,
        Notes = sauceRequest.Notes,
        Recipe = sauceRecipe,
    };
    db.Sauces.Add(sauce);
    db.SaveChanges();
    return Ok(id);
});

app.MapGet("/sauce", () =>
{
    using var db = new SaucesContext();
    Console.WriteLine($"Database path: {db.DbPath}");
    var sauces = db.Sauces
        .Include(x => x.Recipe)
            .ThenInclude(r => r.Fermentations)
                .ThenInclude(f => f.FermentationRecipe)
                    .ThenInclude(f => f.Ingredients)
                        .ThenInclude(i => i.Ingredient)
        .Include(x => x.Recipe)
            .ThenInclude(r => r.NonFermentedIngredients)
                .ThenInclude(n => n.Ingredient);
    
    foreach (var dbSauce in sauces)
    {
        Console.WriteLine(dbSauce.GetLoggingString());
    }
});

app.Run();

{
}