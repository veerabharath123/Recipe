using Recipe.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Recipe.Models
{
    public class IngredientResponse
    {
        public decimal id { get; set; }
        public DateTime? date { get; set; }
        public TimeSpan? time { get; set; }
        [Required]
        public string ingredient_name { get; set; } = string.Empty;
        [Required]
        public string quantity { get; set; } = string.Empty;
        public decimal? recipe_id { get; set; }
    }
}
