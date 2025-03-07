﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Domain.Abstractions
{
    /// <summary>
    /// Represents a base entity with a unique identifier.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public Guid Id { get; set; }

        public bool Deleted { get; set; }
    }
}
