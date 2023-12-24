using System.ComponentModel.DataAnnotations;

namespace Recipe.Models
{
    public class LoginRequest
    {
        [Required]
        public string? emailOrUsername { get; set; }
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
        public bool rememberMe { get; set; }
    }
}
