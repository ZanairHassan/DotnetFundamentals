namespace WeeklyAssignment.Repositories.Interfaces;

public interface IUnitOfWork
{
    IEmployeeRepository Employees { get; }

    IDesignationRepository Designations { get; }
}