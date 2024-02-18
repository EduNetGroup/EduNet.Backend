using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Branches;

namespace EduNet.Backend.Service.Interfaces.Branches;

public interface IBranchService
{
    Task<bool> RemoveAsync(long id);
    Task<BranchForResultDto> RetrieveByIdAsync(long id);
    Task<BranchForResultDto> AddAsync(BranchForCreationDto dto);
    Task<BranchForResultDto> ModifyAsync(long id, BranchForUpdateDto dto);
    Task<IEnumerable<BranchForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<BranchForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
