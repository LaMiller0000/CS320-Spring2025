using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Controllers;
using MyCookBookApp.Models;
using MyCookBookApp.Services;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace MyCookBookApp.Tests
{
    public class RecipeControllerTests
    {
        private readonly Mock<RecipeService> _recipeServiceMock;
        private readonly RecipeController _controller;

        public RecipeControllerTests()
        {
            _recipeServiceMock = new Mock<RecipeService>();
            _controller = new RecipeController(_recipeServiceMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditRecipe_ValidData_ReturnsSuccessJson()
        {
            var recipe = new Recipe
            {
                Name = "Test Recipe",
                Ingredients = new List<string> { "Ingredient1", "Ingredient2" },
                Instructions = new List<string> { "Step1", "Step2" },
                Summary = "Summary",
                Categories = new List<string> { "Category1" }
            };

            _recipeServiceMock.Setup(service => service.UpdateRecipeAsync(recipe)).ReturnsAsync(true);
            var result = await _controller.EditRecipe("1", recipe) as JsonResult;
            var jsonResponse = result.Value as dynamic;

            Assert.True(jsonResponse.success);
            Assert.Equal("Recipe updated successfully", jsonResponse.message);
        }

        [Fact]
        public async Task GetRecipeById_RecipeExists_ReturnsRecipeJson()
        {
            var recipe = new Recipe { Name = "Test Recipe" };
            _recipeServiceMock.Setup(service => service.GetRecipeByIdAsync("1")).ReturnsAsync(recipe);
            var result = await _controller.GetRecipeById("1") as JsonResult;
            Assert.Equal(recipe, result.Value);
        }

        [Fact]
        public async Task GetRecipeById_RecipeNotFound_ReturnsNotFound()
        {
            _recipeServiceMock.Setup(service => service.GetRecipeByIdAsync("1")).ReturnsAsync((Recipe)null);
            var result = await _controller.GetRecipeById("1");
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRecipe_ValidId_ReturnsSuccessJson()
        {
            _recipeServiceMock.Setup(service => service.DeleteRecipeAsync("1")).ReturnsAsync(true);
            var result = await _controller.DeleteRecipe("1") as JsonResult;
            var jsonResponse = result.Value as dynamic;

            Assert.True(jsonResponse.success);
            Assert.Equal("Recipe deleted successfully", jsonResponse.message);
        }

        [Fact]
        public async Task SearchRecipes_ReturnsMatchingRecipes()
        {
            var searchRequest = new RecipeSearchRequest { Query = "Test" };
            var recipes = new List<Recipe> { new Recipe { Name = "Test Recipe" } };

            _recipeServiceMock.Setup(service => service.SearchRecipesAsync(searchRequest)).ReturnsAsync(recipes);
            var result = await _controller.SearchRecipes(searchRequest) as JsonResult;
            Assert.Equal(recipes, result.Value);
        }

        [Fact]
        public async Task AddRecipe_ValidData_ReturnsSuccessJson()
        {
            var recipe = new Recipe
            {
                Name = "New Recipe",
                Ingredients = new List<string> { "Ingredient1", "Ingredient2" },
                Instructions = new List<string> { "Step1", "Step2" },
                Summary = "Summary",
                Categories = new List<string> { "Category1" }
            };

            _recipeServiceMock.Setup(service => service.AddRecipeAsync(recipe)).ReturnsAsync(true);
            var result = await _controller.AddRecipe(recipe) as JsonResult;
            var jsonResponse = result.Value as dynamic;

            Assert.True(jsonResponse.success);
            Assert.Equal("Recipe added successfully", jsonResponse.message);
        }
    }
}