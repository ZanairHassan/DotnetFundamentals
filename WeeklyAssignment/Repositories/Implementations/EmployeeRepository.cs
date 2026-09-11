using WeeklyAssignment.Data;
using WeeklyAssignment.Models;
using WeeklyAssignment.Repositories.Interfaces;

namespace WeeklyAssignment.Repositories.Implementations;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly InMemoryDataStore _dataStore;

    public EmployeeRepository(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _dataStore.Employees;
    }

    public Employee? GetById(int id)
    {
        return _dataStore.Employees.FirstOrDefault(employee => employee.Id == id);
    }

    public void Add(Employee employee)
    {
        _dataStore.Employees.Add(employee);
    }

    public bool Update(Employee employee)
    {
        var existingEmployee = GetById(employee.Id);

        if (existingEmployee is null)
        {
            return false;
        }

        existingEmployee.Name = employee.Name;
        existingEmployee.Email = employee.Email;
        existingEmployee.Salary = employee.Salary;
        existingEmployee.JoiningDate = employee.JoiningDate;
        existingEmployee.DesignationId = employee.DesignationId;
        return true;
    }

    public bool Delete(int id)
    {
        var employee = GetById(id);

        if (employee is null)
        {
            return false;
        }

        return _dataStore.Employees.Remove(employee);
    }

    public bool Exists(int id)
    {
        return _dataStore.Employees.Any(employee => employee.Id == id);
    }
}