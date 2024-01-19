using Microsoft.EntityFrameworkCore;
using Sauces.Core;
using Sauces.Core.Model;

namespace Sauces.Api.Tests.FixturesAndUtils;

public class SaucesContextFixture
{
    public SaucesContext Context { get; init; }
    public const string Name = "TestSauce";
    public FermentationRecipe Recipe1 { get; private set; }
    public FermentationRecipe Recipe2 { get; private set; }
    public FermentationRecipe Recipe3 { get; private set; }
    public Sauce Sauce1 { get; private set; }
    public Sauce Sauce2 { get; private set; }
    public Sauce Sauce3 { get; private set; }

    public SaucesContextFixture()
    {
        var options = new DbContextOptionsBuilder<SaucesContext>().UseInMemoryDatabase(Name).Options;
        Context = new SaucesContext(options);
        InitialiseDbEntries();
    }

    private void InitialiseDbEntries()
    {
        Recipe1 = new FermentationRecipeBuilder().FromDefault().Build();
        Recipe2 = new FermentationRecipeBuilder().FromDefault().Build();
        Recipe3 = new FermentationRecipeBuilder().FromDefault().Build();

        Sauce1 = new SauceBuilder().FromDefault().WithFermentationRecipe(Recipe1).Build();
        Sauce2 = new SauceBuilder().FromDefault().WithFermentationRecipe(Recipe2).Build();
        Sauce3 = new SauceBuilder().FromDefault().WithFermentationRecipe(Recipe3).Build();
        
        Context.Sauces.AddRange(Sauce1, Sauce2, Sauce3);
        Context.FermentationRecipes.AddRange(Recipe1, Recipe2, Recipe3);
        Context.SaveChanges();
    }
    
}