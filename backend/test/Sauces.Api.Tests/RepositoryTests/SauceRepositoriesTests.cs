using FluentAssertions;
using Sauces.Api.Models;
using Sauces.Api.Repositories;
using Sauces.Api.Tests.FixturesAndUtils;
using Sauces.Api.Tests.FixturesAndUtils.Builders;

namespace Sauces.Api.Tests.RepositoryTests;

[TestFixture]
public class SauceRepositoriesTests
{
    private SaucesContextFixture _fixture;

    private SauceRepository GetSut() => new(_fixture.Context);

    [SetUp]
    public void Setup()
    {
        _fixture = new SaucesContextFixture();
    }

    [Test]
    public async Task GetSauce_RetrievesSauceWithAllProperties_WhenItExists()
    {
        var expectedSauce = _fixture.Sauce;
        var sut = GetSut();
        var sauce = await sut.GetAsync(expectedSauce.Id);
        sauce.Should().BeEquivalentTo(expectedSauce);
    }

    [Test]
    public async Task GetSauce_ReturnsNull_WhenIdDoesntExist()
    {
        var id = Guid.NewGuid();
        var sauce = await GetSut().GetAsync(id);
        sauce.Should().BeNull();
    }

    [Test]
    public async Task CreateSauce_Succeeds()
    {
        var recipe = _fixture.Recipe;
        var request = new SauceRequestBuilder().FromDefault().WithRecipeId(recipe.Id).Build();
        var sut = GetSut();
        var sauceId = await sut.CreateAsync(request);
        sauceId.Should().NotBeNull();
        if (sauceId is null)
        {
            return;
        }

        var sauce = await sut.GetAsync(sauceId.Value);
        sauce.Should().NotBeNull();
        sauce?.Name.Should().Be(request.Name);
        sauce?.Fermentation.FermentationRecipe.Should().BeEquivalentTo(recipe);
        sauce?.Notes.Should().Be(request.Notes);
        sauce?.NonFermentedIngredients
            .Select(i => new IngredientModel
            {
                Ingredient = i.Ingredient.Name,
                Percentage = i.Percentage
            })
            .Should().BeEquivalentTo(request.NonFermentedIngredients);
    }

    [Test]
    public async Task UpdateSauce_NewName_JustChangesName()
    {
        var ogSauce = _fixture.Sauce;
        var newName = "new name";
        var updateSauceRequest = new SauceUpdateRequestBuilder().FromSauce(ogSauce).WithName(newName).Build();

        var sut = GetSut();
        await sut.UpdateAsync(ogSauce.Id, updateSauceRequest);
        var updatedSauce = await sut.GetAsync(ogSauce.Id); 
        updatedSauce.Should().NotBeNull();
        if (updatedSauce is null)
        {
            return;
        }
        updatedSauce.Name.Should().Be(newName);
        updatedSauce.LastUpdated.Should().BeAfter(ogSauce.LastUpdated);
        updatedSauce.Created.Should().Be(ogSauce.Created);
        updatedSauce.Fermentation.Should().BeEquivalentTo(ogSauce.Fermentation);
        updatedSauce.Notes.Should().BeEquivalentTo(ogSauce.Notes);
        updatedSauce.Id.Should().Be(ogSauce.Id);
    }

    [Test]
    public async Task UpdateSauce_NoChanges_DoesntChangeUpdatedAt()
    {
        var ogSauce = _fixture.Sauce;
        var updateRequest = new SauceUpdateRequestBuilder().FromSauce(ogSauce).Build();
        var updatedSauce = await GetSut().UpdateAsync(ogSauce.Id, updateRequest);
        updatedSauce.Should().NotBeNull();
        updatedSauce?.LastUpdated.Should().Be(ogSauce.LastUpdated);
    }

    [Test]
    public async Task UpdateSauce_WithNewSauce_DoesntThrow()
    {
        var ogSauce = _fixture.Sauce;
        var recipeRequest = new FermentationRecipeUpdateRequestBuilder()
            .WithId(_fixture.RecipeId)
            .WithIngredients(new IngredientModel { Ingredient = Guid.NewGuid().ToString(), Percentage = 100 })
            .WithLengthInDays(_fixture.Recipe.LengthInDays + 10)
            .Build();
        var sauceRequest = new SauceUpdateRequestBuilder()
            .WithName(ogSauce.Name + " new")
            .WithFermentationPercentage(ogSauce.Fermentation.Percentage + 10)
            .WithNotes(ogSauce.Notes + "extra")
            .WithNonFermentedIngredients(new IngredientModel
                { Ingredient = Guid.NewGuid().ToString(), Percentage = 100 })
            .WithFermentationRecipe(recipeRequest)
            .Build();
        var updateSauce = async () => await GetSut().UpdateAsync(_fixture.SauceId, sauceRequest);
        await updateSauce.Should().NotThrowAsync();
    }
    
    [Test]
    public async Task UpdateSauce_ChangedRecipe_UpdatesSauceAndRecipe()
    {
        var ogSauce = _fixture.Sauce;
        var newIngredient = new IngredientModel { Ingredient = Guid.NewGuid().ToString(), Percentage = 100 };

        var fermentationRecipe = new FermentationRecipeUpdateRequestBuilder()
            .FromFermentationRecipe(ogSauce.Fermentation.FermentationRecipe)
            .WithIngredients(newIngredient)
            .Build();
        var updateRequest = new SauceUpdateRequestBuilder()
            .FromSauce(ogSauce)
            .WithFermentationRecipe(fermentationRecipe)
            .Build();

        var updatedSauce = await GetSut().UpdateAsync(ogSauce.Id, updateRequest);
        updatedSauce.Should().NotBeNull();
        if(updatedSauce is null) return;
        updatedSauce.Name.Should().Be(ogSauce.Name);
        updatedSauce.LastUpdated.Should().BeAfter(ogSauce.LastUpdated);
        updatedSauce.Fermentation.FermentationRecipe.Ingredients.Should().ContainSingle().And.AllSatisfy(i =>
        {
            i.Ingredient.Name.Should().Be(newIngredient.Ingredient);
            i.Percentage.Should().Be(newIngredient.Percentage);
        });
        updatedSauce.Fermentation.FermentationRecipe.LastUpdate.Should()
            .BeAfter(ogSauce.Fermentation.FermentationRecipe.LastUpdate);
    }
    
    [Test]
    public async Task DeleteSauce_RemovesSauceFromDb()
    {
        var id = _fixture.Sauce.Id;
        var sut = GetSut();
        await sut.DeleteAsync(id);
        var sauce = await sut.GetAsync(id);
        sauce.Should().BeNull();
    }
}