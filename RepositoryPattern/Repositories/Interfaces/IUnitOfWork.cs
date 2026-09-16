using RepositoryPattern.Repositories.Interfaces;

namespace RepositoryPattern.Repositories.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    ICareerRepository Careers { get; }
}