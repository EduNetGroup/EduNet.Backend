using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Payments;

namespace EduNet.Backend.Service.Interfaces.Payments;

public interface IPaymentService
{
    Task<bool> RemoveAsync(long id);
    Task<PaymentForResultDto> RetrieveByIdAsync(long id);
    Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto);
    Task<PaymentForResultDto> ModifyAsync(long id, PaymentForUpdateDto dto);
    Task<IEnumerable<PaymentForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<PaymentForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
