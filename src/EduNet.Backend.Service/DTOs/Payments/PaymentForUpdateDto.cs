namespace EduNet.Backend.Service.DTOs.Payments;

public class PaymentForUpdateDto
{
    public long StudentId { get; set; }
    public decimal Amount { get; set; }
    public string Date { get; set; }
}
