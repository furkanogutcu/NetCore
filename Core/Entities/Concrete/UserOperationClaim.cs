using System;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IAuditedEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
