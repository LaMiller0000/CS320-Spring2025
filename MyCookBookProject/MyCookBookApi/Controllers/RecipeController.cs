/*using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{

    private static readonly List<Recipe> Recipes = new List<Recipe>
    {
        new Recipe { Name = "Pasta", Ingredients = new List<string> { "Pasta",
        "Tomato Sauce" }, Steps = "Boil pasta and mix with sauce." },
        new Recipe { Name = "Salad", Ingredients = new List<string> { "Lettuce",
        "Tomatoes", "Cucumbers" }, Steps = "Chop and mix ingredients." }
    };

    [HttpGet]
    public IActionResult GetRecipes()
    {
        return Ok(Recipes);
    }
    [HttpPost("search")]
    public IActionResult Search([FromBody] RecipeSearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
        {
            return BadRequest("Query cannot be empty.");
        }
        var results = Recipes
        .Where(r => r.Name.Contains(request.Query,
        System.StringComparison.OrdinalIgnoreCase))
        .ToList();
        return Ok(results);
    }
}
*/
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
namespace MyCookBookApi.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRecipe(string id, [FromBody] Recipe recipe)
        {
        if (recipe == null || string.IsNullOrWhiteSpace(recipe.Name))
        {
        return BadRequest("Invalid recipe data.");
        }
        var updated = _recipeService.UpdateRecipe(id, recipe);
        if (!updated)
        {
        return NotFound();
        }
        return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(string id)
        {
            var deleted = _recipeService.DeleteRecipe(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }





        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetAllRecipes()
        {
            return Ok(_recipeService.GetAllRecipes());
        }
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipeById(string id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }
        [HttpPost("search")]
        public ActionResult<IEnumerable<Recipe>> SearchRecipes([FromBody]
        RecipeSearchRequest searchRequest)
        {
            if (searchRequest == null ||
            string.IsNullOrWhiteSpace(searchRequest.Keyword))
            {
            return BadRequest("Invalid search request.");
            }
        // Ensure Categories is never null
            searchRequest.Categories ??= new List<CategoryType>();
            var recipes = _recipeService.SearchRecipes(searchRequest);
            return Ok(recipes);
        }
    [HttpPost]
    public ActionResult<Recipe> CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null || string.IsNullOrWhiteSpace(recipe.Name))
            {
                return BadRequest("Invalid recipe data.");
            }
            // Ensure RecipeId is created in the backend
            recipe.RecipeId = Guid.NewGuid().ToString();
            _recipeService.AddRecipe(recipe);
            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.RecipeId
                }, recipe);
        }
    }           
}