using RepositoryPattern.Repositories.Interfaces;

namespace RepositoryPattern.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository Users { get; }

    public ICareerRepository Careers { get; }

    public UnitOfWork(IUserRepository users, ICareerRepository careers)
    {
        Users = users;
        Careers = careers;
    }
}