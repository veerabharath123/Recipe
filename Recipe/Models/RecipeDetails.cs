using System.ComponentModel.DataAnnotations;

namespace Recipe.Models
{
    public class RecipeDetails:Recipe
    {
        public string? Image { get; set; }
        public int TotalIngredients { get; set; }
        [Required,MinLength(1)]
        public List<Ingredient> Ingredients { get; set; }

        public RecipeDetails()
        {
            Ingredients = new List<Ingredient>();
        }
    }
}
