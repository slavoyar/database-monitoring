using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    /// <summary>
    /// Model for Authentication Response
    /// </summary>
    public class WebResponse
    {
        /// <summary>
        /// Common status of response, usually Success|Error
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        /// <summary>
        /// Main string data for response message
        /// </summary>
        [Required(ErrorMessage = "Message is required")]
        public string? Message { get; set; }
    }
}