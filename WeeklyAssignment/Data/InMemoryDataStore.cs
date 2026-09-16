using WeeklyAssignment.Models;

namespace WeeklyAssignment.Data;

public class InMemoryDataStore
{
    public List<Employee> Employees { get; }

    public List<Designation> Designations { get; }

    public InMemoryDataStore()
    {
        Designations = DesignationSeedData.GetDesignations();

        Employees = EmployeeSeedData.GetEmployees();
    }
}