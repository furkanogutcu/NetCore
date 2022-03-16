using System;

namespace Core.Entities
{
    public interface ISoftDeleteEntity : IEntity
    {
        public DateTime? DeletedAt { get; set; }
    }
}
