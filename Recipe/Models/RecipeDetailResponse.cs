using System.ComponentModel.DataAnnotations;

namespace Recipe.Models
{
    public class RecipeDetailResponse:RecipeResponse
    {
        public string image { get; set; } = string.Empty;
        public int totalingredients { get; set; }
        [Required, MinLength(1)]
        public List<IngredientResponse> ingredients { get; set; } = new List<IngredientResponse>();
    }
}
