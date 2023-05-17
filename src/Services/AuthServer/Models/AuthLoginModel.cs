using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    /// <summary>
    /// Model for User Authentication
    /// </summary>
    public class AuthLoginModel
    {
        /// <summary>
        /// Email string data (must include @)
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        /// <summary>
        /// Password string data
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}