using WeeklyAssignment.Data;
using WeeklyAssignment.Models;
using WeeklyAssignment.Repositories.Interfaces;

namespace WeeklyAssignment.Repositories.Implementations;

public class DesignationRepository : IDesignationRepository
{
    private readonly InMemoryDataStore _dataStore;

    public DesignationRepository(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public IEnumerable<Designation> GetAll()
    {
        return _dataStore.Designations;
    }

    public Designation? GetById(int id)
    {
        return _dataStore.Designations.FirstOrDefault(designation => designation.Id == id);
    }
}