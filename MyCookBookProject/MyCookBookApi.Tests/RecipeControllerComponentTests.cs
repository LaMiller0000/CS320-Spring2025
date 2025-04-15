using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
using MyCookBookApi.Controllers;
using MyCookBookApi.Repositories;
using System;

namespace MyCookBookApi.Tests.Controllers
{
    public class RecipeControllerComponentTests
    {
        private readonly RecipeController _controller;

        public RecipeControllerComponentTests()
        {
            var repo = new MockRecipeRepository();           // Fake repo
            var service = new RecipeService(repo);           // Real service
            _controller = new RecipeController(service);     // Real controller
        }

        [Fact]
        public void GetAllRecipes_ReturnsOk_WithRecipes()
        {
            var result = _controller.GetAllRecipes();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var recipes = Assert.IsAssignableFrom<IEnumerable<Recipe>>(okResult.Value);
            Assert.NotEmpty(recipes);
        }

        [Fact]
        public void CreateRecipe_WithValidData_ReturnsCreatedAt()
        {
            var recipe = new Recipe
            {
                Name = "Component Test Recipe",
                Summary = "Test summary",
                Ingredients = new List<string> { "Flour" },
                Instructions = new List<string> { "Mix" }
            };

            var result = _controller.CreateRecipe(recipe);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<Recipe>(created.Value);
            Assert.Equal("Component Test Recipe", returned.Name);
        }

        // Add more for GetById, Update, Delete, etc.
    }
}
