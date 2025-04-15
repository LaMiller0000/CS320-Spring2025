using MyCookBookApi.Models;
using MyCookBookApi.Repositories;
using MyCookBookApi.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyCookBookApi.Tests.Services
{
    public class RecipeServiceTests
    {
        private readonly RecipeService _recipeService;

        public RecipeServiceTests()
        {
            var mockRepo = new MockRecipeRepository();
            _recipeService = new RecipeService(mockRepo);
        }

        [Fact]
        public void GetAllRecipes_ShouldReturnAllRecipes()
        {
            var recipes = _recipeService.GetAllRecipes();

            Assert.NotNull(recipes);
            Assert.True(recipes.Count >= 3); // Based on mock data
        }

        [Fact]
        public void GetRecipeById_ValidId_ReturnsRecipe()
        {
            var all = _recipeService.GetAllRecipes();
            var existingId = all[0].RecipeId;

            var recipe = _recipeService.GetRecipeById(existingId);

            Assert.NotNull(recipe);
            Assert.Equal(existingId, recipe.RecipeId);
        }

        [Fact]
        public void GetRecipeById_InvalidId_ReturnsNull()
        {
            var recipe = _recipeService.GetRecipeById("invalid-id");

            Assert.Null(recipe);
        }

        [Fact]
        public void SearchRecipes_WithKeyword_ReturnsMatchingRecipes()
        {
            var request = new RecipeSearchRequest
            {
                Keyword = "pasta"
            };

            var results = _recipeService.SearchRecipes(request);

            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.Name.Contains("Pasta", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void AddRecipe_ValidRecipe_AddsSuccessfully()
        {
            var recipe = new Recipe
            {
                RecipeId = Guid.NewGuid().ToString(),
                Name = "Test Recipe",
                Ingredients = new List<string> { "Ingredient1" },
                Instructions = new List<string> { "Step1" },
                Summary = "Test Summary",
                TagLine = "Test Tag",
                Categories = new List<CategoryType> { CategoryType.Snack }
            };

            _recipeService.AddRecipe(recipe);

            var all = _recipeService.GetAllRecipes();
            Assert.Contains(all, r => r.Name == "Test Recipe");
        }

        [Fact]
        public void UpdateRecipe_ValidId_UpdatesSuccessfully()
        {
            var recipe = _recipeService.GetAllRecipes()[0];
            var updatedRecipe = new Recipe
            {
                RecipeId = recipe.RecipeId,
                Name = "Updated Name",
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                Summary = recipe.Summary,
                TagLine = recipe.TagLine,
                Categories = recipe.Categories
            };

            var result = _recipeService.UpdateRecipe(recipe.RecipeId, updatedRecipe);

            Assert.True(result);
            var modified = _recipeService.GetRecipeById(recipe.RecipeId);
            Assert.Equal("Updated Name", modified.Name);
        }

        [Fact]
        public void DeleteRecipe_ValidId_DeletesSuccessfully()
        {
            var recipe = _recipeService.GetAllRecipes()[0];

            var result = _recipeService.DeleteRecipe(recipe.RecipeId);

            Assert.True(result);
            Assert.Null(_recipeService.GetRecipeById(recipe.RecipeId));
        }

        [Fact]
        public void DeleteRecipe_InvalidId_ReturnsFalse()
        {
            var result = _recipeService.DeleteRecipe("nonexistent-id");

            Assert.False(result);
        }
    }
}
