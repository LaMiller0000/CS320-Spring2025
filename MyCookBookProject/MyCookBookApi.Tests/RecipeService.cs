using MyCookBookApi.Models;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace MyCookBookApi.Tests.Models
{
    public class RecipeModelTests
    {
        [Fact]
        public void Recipe_DefaultConstructor_ShouldInitializeLists()
        {
            var recipe = new Recipe();

            Assert.NotNull(recipe.Ingredients);
            Assert.NotNull(recipe.Instructions);
            Assert.NotNull(recipe.Categories);
            Assert.NotNull(recipe.Media);
        }

        [Fact]
        public void Recipe_CanSetAndRetrieveValues()
        {
            var recipe = new Recipe
            {
                Name = "Test",
                Summary = "Summary",
                Ingredients = new List<string> { "i1", "i2" },
                Instructions = new List<string> { "s1" },
                Categories = new List<CategoryType> { CategoryType.Snack }
            };

            Assert.Equal("Test", recipe.Name);
            Assert.Equal("Summary", recipe.Summary);
            Assert.Contains("i1", recipe.Ingredients);
            Assert.Single(recipe.Instructions);
            Assert.Contains(CategoryType.Snack, recipe.Categories);
        }

        [Fact]
        public void RecipeSearchRequest_DefaultConstructor_ShouldInitializeCategories()
        {
            var request = new RecipeSearchRequest();

            Assert.NotNull(request.Categories);
            Assert.Empty(request.Categories);
        }

        // CategoryTypeConverter Test
        [Fact]
        public void Recipe_SerializesAndDeserializes_Correctly()
        {
            var recipe = new Recipe
            {
                Name = "SerializeTest",
                Categories = new List<CategoryType> { CategoryType.Breakfast }
            };

            var json = JsonSerializer.Serialize(recipe);
            var deserialized = JsonSerializer.Deserialize<Recipe>(json);

            Assert.Equal("SerializeTest", deserialized.Name);
            Assert.Contains(CategoryType.Breakfast, deserialized.Categories);
        }

    }
}

