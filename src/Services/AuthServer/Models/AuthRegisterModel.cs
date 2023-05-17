using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    // Username (not FullUserName) must == Email in database !

    /// <summary>
    /// Model for user Register
    /// </summary>
    public class AuthRegisterModel : AuthLoginModel
    {
        /// <summary>
        /// Custom user identification data
        /// </summary>
        [Required(ErrorMessage = "FullUserName is required")]
        public string? FullUserName { get; set; }

        /// <summary>
        /// Certain Role from UserRoles class (Admin,Manager,Engineer)
        /// </summary>
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }

        /// <summary>
        /// Phone Number for user notification
        /// </summary>
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string? PhoneNumber { get; set; }
    }
}