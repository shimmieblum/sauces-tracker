using FluentAssertions;
using Sauces.Api.Models;
using Sauces.Api.Repositories;
using Sauces.Api.Tests.FixturesAndUtils;

namespace Sauces.Api.Tests;

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
        var expectedSauce = _fixture.Sauce1;
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
        var recipe = _fixture.Recipe1;
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
            .Select(i => new IngredientsModel
            {
                Ingredient = i.Ingredient.Name,
                Percentage = i.Percentage
            })
            .Should().BeEquivalentTo(request.NonFermentedIngredients);
    }

    [Test]
    public async Task DeleteSauce_RemovesSauceFromDb()
    {
        var id = _fixture.Sauce1.Id;
        var sut = GetSut();
        await sut.DeleteAsync(id);
        var sauce = await sut.GetAsync(id);
        sauce.Should().BeNull();
    }
}