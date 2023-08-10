using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    /// <summary>
    /// Model for user Register
    /// </summary>
    public class AuthUpdateModel
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Custom user identification data
        /// </summary>
        public string? FullUserName { get; set; }

        /// <summary>
        /// Phone Number for user notification
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email for user notification
        /// </summary>
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public string? Role { get; set; }

        public string? Password { get; set; }
    }
}