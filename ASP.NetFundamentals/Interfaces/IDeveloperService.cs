using ASP.NetFundamentals.Models;

namespace ASP.NetFundamentals.Interfaces
{
    public interface IDeveloperService
    {
        IEnumerable<Developer> GetAll();

        Developer? GetById(int id);

        void Create(Developer developer);

        bool Update(int id, Developer developer);

        bool Delete(int id);
    }
}