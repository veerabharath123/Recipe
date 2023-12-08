using Recipe.Helpers;

namespace Recipe.Models
{
    public class RecipeType:BaseClass<decimal>
    {
        public string? RECIPETYPE_NAME { get; set; }
    }
}
