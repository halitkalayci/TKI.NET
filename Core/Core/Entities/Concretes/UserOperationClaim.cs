namespace Core.Entities.Concretes
{
    public class UserOperationClaim : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}

