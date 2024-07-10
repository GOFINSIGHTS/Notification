using System.ComponentModel.DataAnnotations;

namespace Notification.Models
{
    /// <summary>
    /// Internal model for representing a role.
    /// </summary>
    public sealed class UserModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
