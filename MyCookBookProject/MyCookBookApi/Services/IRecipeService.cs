using MyCookBookApi.Models;
using System.Collections.Generic;
namespace MyCookBookApi.Services
{
    public interface IRecipeService
    {
        List<Recipe> GetAllRecipes();
        Recipe GetRecipeById(string id);

        bool DeleteRecipe(string id);
        List<Recipe> SearchRecipes(RecipeSearchRequest searchRequest);

        bool UpdateRecipe(string id, Recipe recipe);
        void AddRecipe(Recipe recipe);
    }
}