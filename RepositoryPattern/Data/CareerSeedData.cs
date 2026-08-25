using RepositoryPattern.Models;

namespace RepositoryPattern.Data;

public static class CareerSeedData
{
    public static List<Career> GetCareers()
    {
        return new List<Career>
        {
            new()
            {
                Id = 1,
                Title = "Software Engineer",
                Description = "Designs, develops, tests, and maintains software applications."
            },

            new()
            {
                Id = 2,
                Title = "QA Engineer",
                Description = "Tests software applications to identify defects and ensure quality."
            },

            new()
            {
                Id = 3,
                Title = "Project Manager",
                Description = "Plans, coordinates, and manages software development projects."
            },

            new()
            {
                Id = 4,
                Title = "UI/UX Designer",
                Description = "Designs user interfaces and user experiences for applications."
            },

            new()
            {
                Id = 5,
                Title = "DevOps Engineer",
                Description = "Manages deployment, infrastructure, automation, and CI/CD processes."
            }
        };
    }
}