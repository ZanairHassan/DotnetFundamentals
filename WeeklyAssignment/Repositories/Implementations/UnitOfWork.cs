using WeeklyAssignment.Data;
using WeeklyAssignment.Repositories.Interfaces;

namespace WeeklyAssignment.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly InMemoryDataStore _dataStore;

    private IEmployeeRepository? _employees;

    private IDesignationRepository? _designations;

    public UnitOfWork(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public IEmployeeRepository Employees => _employees ??= new EmployeeRepository(_dataStore);

    public IDesignationRepository Designations => _designations ??= new DesignationRepository(_dataStore);
}