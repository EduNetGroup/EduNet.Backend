namespace EduNet.Backend.Service.DTOs.Payments;

public class PaymentForUpdateDto
{
    public long StudentId { get; set; }
    public long BranchId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
