using RepositoryPattern.Models;
using RepositoryPattern.Repositories.Interfaces;
using RepositoryPattern.Services.Interfaces;

namespace RepositoryPattern.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await _unitOfWork.Users.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Users.GetByIdAsync(id);
    }

    public async Task<User?> CreateAsync(User user)
    {
        Career? career = await _unitOfWork.Careers.GetByIdAsync(user.CareerId);

        if (career is null)
        {
            return null;
        }

        return await _unitOfWork.Users.CreateAsync(user);
    }

    public async Task<User?> UpdateAsync(User user)
    {
        Career? career = await _unitOfWork.Careers
            .GetByIdAsync(user.CareerId);

        if (career is null)
        {
            return null;
        }

        return await _unitOfWork.Users.UpdateAsync(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _unitOfWork.Users.DeleteAsync(id);
    }
}
