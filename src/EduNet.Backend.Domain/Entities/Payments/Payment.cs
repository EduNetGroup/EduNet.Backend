using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Domain.Entities.Students;

namespace EduNet.Backend.Domain.Entities.Payments;

public class Payment : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public decimal Amount { get; set; }
    public string Date { get; set; }
}
