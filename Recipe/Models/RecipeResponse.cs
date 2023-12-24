﻿using Recipe.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Recipe.Models
{
    public class RecipeResponse
    {
        public decimal id { get; set; }
        [Required]
        public string recipe_name { get; set; } = string.Empty;
        public decimal? recipe_type_id { get; set; }
        public string recipe_type_name { get; set; } = string.Empty;
        [Required]
        public string description { get; set; } = string.Empty;
        public string favourites { get; set; } = string.Empty;
        public decimal user_id { get; set; }
        public Guid? image_id { get; set; }
    }
}
