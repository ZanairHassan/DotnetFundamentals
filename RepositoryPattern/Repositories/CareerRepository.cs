using RepositoryPattern.Data;
using RepositoryPattern.Models;
using RepositoryPattern.Repositories.Interfaces;

namespace RepositoryPattern.Repositories;

public class CareerRepository : ICareerRepository
{
    private readonly List<Career> _careers;

    public CareerRepository(InMemoryDataStore dataStore)
    {
        _careers = dataStore.Careers;
    }

    public Task<IReadOnlyList<Career>> GetAllAsync()
    {
        IReadOnlyList<Career> careers = _careers.ToList();

        return Task.FromResult(careers);
    }

    public Task<Career?> GetByIdAsync(int id)
    {
        Career? career = _careers.FirstOrDefault(career => career.Id == id);

        return Task.FromResult(career);
    }

    public Task<Career?> CreateAsync(Career career)
    {
        bool titleExists = _careers
            .Any(existingCareer =>existingCareer.Title
            .Equals(career.Title,StringComparison.OrdinalIgnoreCase));

        if (titleExists)
        {
            return Task.FromResult<Career?>(null);
        }

        int nextId = _careers.Count == 0 ? 1 : _careers.Max(career => career.Id) + 1;

        career.Id = nextId;

        _careers.Add(career);

        return Task.FromResult<Career?>(career);
    }

    public Task<Career?> UpdateAsync(Career career)
    {
        Career? existingCareer = _careers.FirstOrDefault(existingCareer => existingCareer.Id == career.Id);

        if (existingCareer is null)
        {
            return Task.FromResult<Career?>(null);
        }

        bool titleExists = _careers.Any(existingCareer =>
                existingCareer.Id != career.Id &&
                existingCareer.Title.Equals(career.Title, StringComparison.OrdinalIgnoreCase));

        if (titleExists)
        {
            return Task.FromResult<Career?>(null);
        }

        existingCareer.Title = career.Title;
        existingCareer.Description = career.Description;

        return Task.FromResult<Career?>(existingCareer);
    }

    public Task<bool> DeleteAsync(int id)
    {
        Career? career = _careers.FirstOrDefault(career => career.Id == id);

        if (career is null)
        {
            return Task.FromResult(false);
        }

        _careers.Remove(career);

        return Task.FromResult(true);
    }
}