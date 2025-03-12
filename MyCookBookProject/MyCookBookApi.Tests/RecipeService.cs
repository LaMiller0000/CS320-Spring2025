using System.Collections.Generic;
using Xunit;
using MyCookBookApi.Models;
using MyCookBookApi.Repositories;
using MyCookBookApi.Services;
public class RecipeServiceTests
{
    private readonly RecipeService _recipeService;
    public RecipeServiceTests()
    {
        _recipeService = new RecipeService(new MockRecipeRepository());
    }
    [Fact]
    public void GetAllRecipes_ShouldReturnNonEmptyList()
    {
        // Act
        var recipes = _recipeService.GetAllRecipes();
        // Assert
        Assert.NotEmpty(recipes);
    }
    [Fact]
    public void GetRecipeById_ShouldReturnCorrectRecipe()
    {
        // Arrange
        var recipeId = _recipeService.GetAllRecipes()[0].RecipeId;
        // Act
        var recipe = _recipeService.GetRecipeById(recipeId);
        // Assert
        Assert.NotNull(recipe);
        Assert.Equal(recipeId, recipe.RecipeId);
    }
}