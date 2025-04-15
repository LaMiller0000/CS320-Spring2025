using System.Collections.Generic;
using Xunit;
using MyCookBookApi.Models;
using MyCookBookApi.Repositories;
using MyCookBookApi.Services;

namespace MyCookBookApi.Tests
{
    public class RecipeService_IntegrationTests
    {
        [Fact]
        public void SearchRecipes_WithKeywordAndCategory_ReturnsMatchingRecipes()
        {
            // Arrange: Real Repo and Service
            var repository = new MockRecipeRepository();
            var service = new RecipeService(repository);

            var request = new RecipeSearchRequest
            {
                Keyword = "salad",
                Categories = new List<CategoryType> { CategoryType.Vegan }
            };

            // Act
            var result = service.SearchRecipes(request);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // only one recipe should match: "Salad"
            Assert.Equal("Salad", result[0].Name);
        }

        [Fact]
        public void AddRecipe_ThenGetById_ReturnsSameRecipe()
        {
            // Arrange
            var repository = new MockRecipeRepository();
            var service = new RecipeService(repository);

            var newRecipe = new Recipe
            {
                RecipeId = "TEST-001",
                Name = "Smoothie",
                Summary = "A healthy fruit smoothie",
                Ingredients = new List<string> { "Banana", "Milk" },
                Instructions = new List<string> { "Blend all ingredients" },
                Categories = new List<CategoryType> { CategoryType.Breakfast }
            };

            // Act
            service.AddRecipe(newRecipe);
            var result = service.GetRecipeById("TEST-001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Smoothie", result.Name);
            Assert.Contains("Banana", result.Ingredients);
        }

        [Fact]
        public void AddRecipe_ThenGetById_ShouldReturnSameRecipe()
        {
            // Arrange
            var repository = new MockRecipeRepository();
            var service = new RecipeService(repository);

            var testRecipe = new Recipe
            {
                RecipeId = "TEST-001",
                Name = "Chocolate Milkshake",
                Summary = "Sweet and creamy chocolate milkshake",
                Ingredients = new List<string> { "Milk", "Chocolate Syrup", "Ice Cream" },
                Instructions = new List<string> { "Blend all ingredients until smooth." },
                Categories = new List<CategoryType> { CategoryType.Dessert }
            };

            // Act
            service.AddRecipe(testRecipe);
            var result = service.GetRecipeById("TEST-001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Chocolate Milkshake", result.Name);
            Assert.Contains("Milk", result.Ingredients);
            Assert.Equal(CategoryType.Dessert, result.Categories[0]);
        }
    }
}
