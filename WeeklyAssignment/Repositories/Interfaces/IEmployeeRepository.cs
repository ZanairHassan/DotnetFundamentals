using WeeklyAssignment.Models;

namespace WeeklyAssignment.Repositories.Interfaces;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll();

    Employee? GetById(int id);

    void Add(Employee employee);

    bool Update(Employee employee);

    bool Delete(int id);

    bool Exists(int id);
}