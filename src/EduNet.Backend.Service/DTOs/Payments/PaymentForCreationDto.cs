namespace EduNet.Backend.Service.DTOs.Payments;

public class PaymentForCreationDto
{
    public long StudentId { get; set; }
    public long BranchId { get; set; }
    public decimal Amount { get; set; }
    public string Date { get; set; }
}
