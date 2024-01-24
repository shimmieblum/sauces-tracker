using Microsoft.EntityFrameworkCore;
using Sauces.Api.Models.ExtensionsAndUtils;
using Sauces.Api.Repositories;
using Sauces.Api.Tests.FixturesAndUtils.Builders;
using Sauces.Core.Model;

namespace Sauces.Api.Tests.FixturesAndUtils;

public class SaucesContextFixture
{
    public SaucesContext Context { get; }
    private const string Name = "TestSauce";
    public readonly Guid RecipeId = Guid.NewGuid();

    public FermentationRecipe Recipe => 
        Context.FermentationRecipes.IncludeEverything().AsNoTracking().FirstOrDefault(s => s.Id == RecipeId) 
        ?? throw new Exception("error getting sauce in fixture");

    public readonly Guid SauceId = Guid.NewGuid();
    public Sauce Sauce => 
        Context.Sauces.IncludeEverything().AsNoTracking().FirstOrDefault(s => s.Id == SauceId)
        ?? throw new Exception("error getting sauce in fixture");
   
    public SaucesContextFixture()
    {
        var options = new DbContextOptionsBuilder<SaucesContext>()
            .UseInMemoryDatabase(Name)
            .EnableSensitiveDataLogging()
            .Options;
        Context = new SaucesContext(options);
        InitialiseDbEntries();
    }

    private void InitialiseDbEntries()
    {
        var recipe = new FermentationRecipeBuilder().FromDefault().WithId(RecipeId).Build();
        var sauce = new SauceBuilder().FromDefault().WithId(SauceId).WithFermentationRecipe(recipe).Build();
        Context.Sauces.Add(sauce);
        Context.FermentationRecipes.Add(recipe);
        Context.SaveChanges();
    }

    
}