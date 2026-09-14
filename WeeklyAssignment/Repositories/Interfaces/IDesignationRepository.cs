using WeeklyAssignment.Models;

namespace WeeklyAssignment.Repositories.Interfaces;

public interface IDesignationRepository
{
    IEnumerable<Designation> GetAll();

    Designation? GetById(int id);
}