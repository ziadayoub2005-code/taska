using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Entities
{
    public class Payment : BaseEntity
    {
        public int ServiceRequestId { get; set; }
        public PaymentMethod Method { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }

        public ServiceRequest ServiceRequest { get; set; } = null!;
    }
}