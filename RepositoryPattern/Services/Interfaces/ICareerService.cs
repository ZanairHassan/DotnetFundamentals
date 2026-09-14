using RepositoryPattern.Models;

namespace RepositoryPattern.Services.Interfaces;

public interface ICareerService
{
    Task<IReadOnlyList<Career>> GetAllAsync();

    Task<Career?> GetByIdAsync(int id);

    Task<Career?> CreateAsync(Career career);

    Task<Career?> UpdateAsync(Career career);

    Task<bool> DeleteAsync(int id);
}