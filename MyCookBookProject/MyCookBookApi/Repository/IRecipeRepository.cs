using MyCookBookApi.Models;
using System.Collections.Generic;
namespace MyCookBookApi.Repositories
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAllRecipes();
        Recipe GetRecipeById(string id);
        List<Recipe> SearchRecipes(RecipeSearchRequest searchRequest);
        void AddRecipe(Recipe recipe);
    }
}