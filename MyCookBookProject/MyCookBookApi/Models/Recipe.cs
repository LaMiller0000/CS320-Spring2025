using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace MyCookBookApi.Models
{
    [FirestoreData] // 🔥 Marks the class as Firestore-compatible
    public class Recipe
    {
        [FirestoreProperty] public string VideoId { get; set; } // The ID of the YouTube video
        [FirestoreProperty] public string Title { get; set; }   // Title of the video
        [FirestoreProperty] public string Description { get; set; } // Short description
        [FirestoreProperty] public string RecipeId { get; set; } // Auto-generated,
        //cannot be set manually
        [FirestoreProperty] public string Name { get; set; }
        [FirestoreProperty] public string TagLine { get; set; }
        [FirestoreProperty] public string Summary { get; set; }
        [FirestoreProperty] public List<string> Instructions { get; set; } = new List<string>();
        [FirestoreProperty] public List<string> Ingredients { get; set; } = new List<string>();
        [FirestoreProperty(ConverterType = typeof(CategoryTypeConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CategoryType>? Categories { get; set; } = new List<CategoryType>();
        [FirestoreProperty(ConverterType = typeof(RecipeMediaConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RecipeMedia>? Media { get; set; } = new List<RecipeMedia>();
    }
}