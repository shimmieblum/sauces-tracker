using FluentAssertions;
using Microsoft.AspNetCore.Connections;
using Sauces.Api.Models;
using Sauces.Api.Repositories;
using Sauces.Api.Tests.FixturesAndUtils;
using Sauces.Api.Tests.FixturesAndUtils.Builders;

namespace Sauces.Api.Tests.RepositoryTests;


[TestFixture]
public class FermentationRecipeRepositoryTests
{
    private SaucesContextFixture _fixture;
 
    private FermentationRepository GetSut() => new(_fixture.Context);
    [SetUp]
    public void Setup()
    {
        _fixture = new SaucesContextFixture();
    }

    [Test]
    public async Task GetFermentation_ReturnsExpectedEntry_WhenExists()
    {
        var exRecipe = _fixture.Recipe;
        var sut = GetSut();
        var recipe = await sut.GetAsync(exRecipe.Id);
        recipe.Should().BeEquivalentTo(exRecipe);
    }

    [Test]
    public async Task GetFermentation_ReturnsNull_WhenDoesntExist()
    {
        var id = Guid.NewGuid();
        var recipe = await GetSut().GetAsync(id);
        recipe.Should().BeNull();
    }

    [Test]
    public async Task CreateFermentation_InputsNewFermentation()
    {
        var request = new FermentationRequestBuilder().FromDefault().Build();
        var sut = GetSut();
        var id = await sut.CreateAsync(request);
        id.Should().NotBeNull();
        if(id is null) {return;}

        var recipe = await sut.GetAsync(id.Value);
        recipe.Should().NotBeNull();
        recipe?.LengthInDays.Should().Be(request.LengthInDays);
        recipe?.Ingredients.Select(i => new IngredientModel
            {
                Ingredient = i.Ingredient.Name, Percentage = i.Percentage
            })
            .Should().BeEquivalentTo(request.Ingredients);
        recipe?.Id.Should().Be(id.Value);
    }

    [Test]
    public async Task UpdateFermentation_WithNoChange_ChangesNothing()
    {
        var recipe = _fixture.Recipe;
        var request = new FermentationRecipeUpdateRequestBuilder().FromFermentationRecipe(recipe).Build();
        var sut = GetSut();
        await sut.UpdateAsync(recipe.Id, request);
        var updatedRecipe = await sut.GetAsync(recipe.Id);
        updatedRecipe.Should().BeEquivalentTo(recipe);
    }

    [Test]
    public async Task UpdateFermentation_WithChange_ShouldUpdate()
    {
        var recipe = _fixture.Recipe;
        var updatedDays = recipe.LengthInDays + 10;
        var request = new FermentationRecipeUpdateRequestBuilder().FromFermentationRecipe(recipe)
            .WithLengthInDays(updatedDays).Build();
        var sut = GetSut();
        await sut.UpdateAsync(recipe.Id, request);
        var updatedRecipe = await sut.GetAsync(recipe.Id);
        updatedRecipe.Should().NotBeNull();
        updatedRecipe?.LastUpdate.Should().BeAfter(recipe.LastUpdate);
        updatedRecipe?.LengthInDays.Should().Be(updatedDays);
    }
    
    
        
}