using System;

namespace Core.Entities
{
    public interface IAuditedEntity : IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}