
using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRecipes()
    {
    return Ok(new List<Recipe>
        {
        new Recipe { Name = "Pasta", Ingredients = new
        List<string> { "Pasta", "Tomato Sauce" }, Steps = "Boil pasta." },
        new Recipe { Name = "Salad", Ingredients = new
        List<string> { "Lettuce", "Tomatoes" }, Steps = "Mix all ingredients."
        }
        });
    }
}

/*
using RecipeSearchRequest;
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