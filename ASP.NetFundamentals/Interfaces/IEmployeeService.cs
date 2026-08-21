using ASP.NetFundamentals.Models;

namespace ASP.NetFundamentals.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();

        Employee? GetById(int id);

        IEnumerable<Employee> Search(string searchTerm);

        void Add(Employee employee);

        bool Update(Employee employee);

        bool Delete(int id);
    }
}
