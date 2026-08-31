using RepositoryPattern.Models;
using RepositoryPattern.Repositories.Interfaces;
using RepositoryPattern.Services.Interfaces;

namespace RepositoryPattern.Services;

public class CareerService : ICareerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CareerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<Career>> GetAllAsync()
    {
        return await _unitOfWork.Careers.GetAllAsync();
    }

    public async Task<Career?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Careers.GetByIdAsync(id);
    }

    public async Task<Career?> CreateAsync(Career career)
    {
        return await _unitOfWork.Careers.CreateAsync(career);
    }

    public async Task<Career?> UpdateAsync(Career career)
    {
        return await _unitOfWork.Careers.UpdateAsync(career);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        IReadOnlyList<User> users = await _unitOfWork.Users.GetAllAsync();

        var assignedUsers = users.Where(user => user.CareerId == id).ToList();
        if (assignedUsers.Count > 0)
        {
            foreach (var user in assignedUsers)
            {
                user.CareerId = 0;

                await _unitOfWork.Users.UpdateAsync(user);
            }
        }

        return await _unitOfWork.Careers.DeleteAsync(id);
    }
}