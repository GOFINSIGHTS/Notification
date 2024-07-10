using Microsoft.EntityFrameworkCore;
using Notification.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Notification.Domain.Entities
{
    [Index(nameof(Email),IsUnique = true)]
    public sealed class User : BaseEntity
    {
        [EmailAddress]
        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Required]
        public required DateTime Date { get; set; }        
    }
}
