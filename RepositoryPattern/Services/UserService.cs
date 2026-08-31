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

    public async Task<bool> AssignCareerAsync(IReadOnlyCollection<int> userIds, int careerId)
    {
        Career? career = await _unitOfWork.Careers.GetByIdAsync(careerId);

        if (career is null)
        {
            return false;
        }

        List<User> users = [];

        foreach (int userId in userIds)
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user is null)
            {
                return false;
            }

            users.Add(user);
        }

        foreach (User user in users)
        {
            user.CareerId = careerId;

            await _unitOfWork.Users.UpdateAsync(user);
        }

        return true;
    }

    public async Task<IReadOnlyList<Career>> GetCareersAsync()
    {
        return await _unitOfWork.Careers.GetAllAsync();
    }
}
