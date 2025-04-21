/*using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using System.Threading.Tasks;
namespace MyCookBookApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeService _recipeService;
        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetRecipesAsync();
            return View(recipes);
        }

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }
            var recipes = await _recipeService.SearchRecipesAsync(query);
            return View("Index", recipes);
        }
    }
}
*/

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using System.Threading.Tasks;
using MyCookBookApp.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCookBookApp.Controllers
{
    [ApiController]
    [Route("Recipe")]
    public class RecipeController : Controller
    {


        private readonly RecipeService _recipeService;

        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // Show the Recipe Index Page
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditRecipe(string id, [FromBody] Recipe
        recipe)
        {
            if (recipe == null || 
                string.IsNullOrWhiteSpace(recipe.Name) || 
                string.IsNullOrWhiteSpace(recipe.Summary) || 
                recipe.Ingredients == null || recipe.Ingredients.Count == 0 || 
                recipe.Instructions == null || recipe.Instructions.Count == 0 || 
                (recipe.Categories == null || !recipe.Categories.Any()))
            {
                return BadRequest(new { success = false, message = "Invalid recipe data. Ensure all required fields are provided." });
            }
        bool updated = await _recipeService.UpdateRecipeAsync(recipe);
        return Json(new { success = updated, message = updated ? "Recipe updated successfully" : "Failed to update recipe" }); }   



        // Fetch All Recipes (GET /Recipe/GetAll)
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetRecipesAsync();
            return Json(recipes);
        }

        // Fetch Recipe by ID (GET /Recipe/{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(string id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound(new { success = false, message = "Recipe not found" });
            }
            return Json(recipe);
        }

                // Delete a Recipe (DELETE /Recipe/Delete/{id})
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { success = false, message = "Invalid recipe ID" });
            }
            bool deleted = await _recipeService.DeleteRecipeAsync(id);
            return Json(new { success = deleted, message = deleted ? "Recipe deleted successfully" : "Failed to delete recipe" });
        }

        // Search for Recipes (POST /Recipe/Search)
        [HttpPost("Search")]
        public async Task<IActionResult> SearchRecipes([FromBody] RecipeSearchRequest searchRequest)
        {
            // searchRequest.Categories = new List<CategoryType>();
            var recipes = await _recipeService.SearchRecipesAsync(searchRequest);
            return Json(recipes);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRecipe([FromBody] Recipe recipe)
        {
            // Log the incoming recipe object
            Console.WriteLine("Received Recipe: " + JsonConvert.SerializeObject(recipe));

            // Validate the recipe data
            if (recipe == null || string.IsNullOrWhiteSpace(recipe.Name) ||
            recipe.Ingredients == null || recipe.Ingredients.Count == 0 ||
            recipe.Instructions == null || recipe.Instructions.Count == 0 ||
            string.IsNullOrWhiteSpace(recipe.Summary) || recipe.Categories == null)
            {
                return BadRequest(new { success = false, message = "Invalid recipe data" });
            }

        
                // Call the AddRecipeAsync service to save the recipe
                bool added = await _recipeService.AddRecipeAsync(recipe);
                Console.WriteLine(added);

                // Return success or failure response
                return Json(new { 
                    success = added, 
                    message = added ? "Recipe added successfully" : "Failed to add recipe" });
            

                // Handle exceptions and return an error response
                //Console.WriteLine($"Error adding recipe: {ex.Message}");
                //return StatusCode(500, new { success = false, message = "An error occurred while adding the recipe." });
            
        }
    }
}
