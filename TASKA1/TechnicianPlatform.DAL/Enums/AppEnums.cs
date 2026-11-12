namespace TechnicianPlatform.DAL.Enums
{
    public enum UserRole
    {
        Customer,
        Technician,
        Admin
    }

    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected,
        Suspended
    }

    public enum DocumentType
    {
        NationalId,
        Certificate
    }

    public enum RequestStatus
    {
        Pending,
        Accepted,
        InProgress,
        Completed,
        Rejected,
        Cancelled
    }

    public enum ComplaintStatus
    {
        Open,
        Closed
    }

    public enum PaymentMethod
    {
        Cash,
        OnlineSimulated
    }

    public enum EditRequestStatus
    {
        Pending,
        Approved,
        Rejected
    }
}