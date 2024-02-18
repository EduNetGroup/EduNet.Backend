using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Domain.Entities.Students;

namespace EduNet.Backend.Service.DTOs.Payments;

public class PaymentForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public decimal Amount { get; set; }
    public string Date { get; set; }
}
