using Notification.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Domain.EntitiesDTO
{
    public class UserDto : BaseEntityDto
    {       
        public required string Email { get; set; }
                
        public required DateTime Date { get; set; }
    }
}
