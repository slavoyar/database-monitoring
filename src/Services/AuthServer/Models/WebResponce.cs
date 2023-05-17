using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    /// <summary>
    /// Model for Authentication Responce
    /// </summary>
    public class WebResponce
    {
        /// <summary>
        /// Common status of responce, usually Success|Error
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        /// <summary>
        /// Main string data for responce message
        /// </summary>
        [Required(ErrorMessage = "Message is required")]
        public string? Message { get; set; }
    }
}