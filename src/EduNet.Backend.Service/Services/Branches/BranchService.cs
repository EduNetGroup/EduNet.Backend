using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.DTOs.Branches;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Service.Interfaces.Branches;

namespace EduNet.Backend.Service.Services.Branches;

public class BranchService : IBranchService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Branch> _branchRepository;

    public BranchService(IMapper mapper, IRepository<Branch> branchRepository)
    {
        _mapper = mapper;
        _branchRepository = branchRepository;
    }

    public async Task<BranchForResultDto> AddAsync(BranchForCreationDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Name.ToLower() == dto.Name.ToLower() && !b.IsDeleted);
        if (branchData is not null)
            throw new EduNetException(409, "Branch is already exist");

        var mappedData = _mapper.Map<Branch>(dto);

        return _mapper.Map<BranchForResultDto>(await _branchRepository.InsertAsync(mappedData));
    }

    public async Task<BranchForResultDto> ModifyAsync(long id, BranchForUpdateDto dto)
    {
        var branchData = await _branchRepository.
            SelectAsync(b => b.Id == id && !b.IsDeleted);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        var mappedData = _mapper.Map(dto, branchData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _branchRepository.UpdateAsync(mappedData);

        return _mapper.Map<BranchForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == id && !b.IsDeleted);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        return await _branchRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BranchForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var branchData = await _branchRepository
            .SelectAll(b => !b.IsDeleted)
            .Include(b => b.Users.Where(u => !u.IsDeleted))
            .Include(b => b.Courses.Where(c => !c.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BranchForResultDto>>(branchData);
    }

    public async Task<BranchForResultDto> RetrieveByIdAsync(long id)
    {
        var branchData = await _branchRepository
            .SelectAll(b => !b.IsDeleted)
            .Where(b => b.Id == id)
            .Include(b => b.Users.Where(u => !u.IsDeleted))
            .Include(b => b.Courses.Where(c => !c.IsDeleted))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        return _mapper.Map<BranchForResultDto>(branchData);
    }

    public async Task<IEnumerable<BranchForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var branchData = await _branchRepository
            .SelectAll(b => !b.IsDeleted)
            .Where(b => b.Name.ToLower().Contains(search.ToLower())
                || b.Description.ToLower().Contains(search.ToLower())
                || b.Address.ToLower().Contains(search.ToLower())
                || b.PhoneNumber.Contains(search))
            .Include(b => b.Users.Where(u => !u.IsDeleted))
            .Include(b => b.Courses.Where(u => !u.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BranchForResultDto>>(branchData);
    }
}
