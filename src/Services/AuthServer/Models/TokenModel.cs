using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    /// <summary>
    /// Model for Token Data
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// JWT Access Token
        /// </summary>
        [Required(ErrorMessage = "AccessToken is required")]
        public string? AccessToken { get; set; }

        /// <summary>
        /// JWT Refresh Token
        /// </summary>
        [Required(ErrorMessage = "RefreshToken is required")]
        public string? RefreshToken { get; set; }
    }
}