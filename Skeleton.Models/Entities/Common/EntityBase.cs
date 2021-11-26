using Skeleton.Core.Utilities;
using System;

namespace Skeleton.Models.Entities.Common
{
    public abstract class EntityBase <T>
    {
        protected EntityBase()
        {
            this.CreatedAt = DateTimeHelper.LocalTime();
        }

        /// <summary>
        /// Primary Key
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Is Deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
