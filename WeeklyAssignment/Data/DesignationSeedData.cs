using WeeklyAssignment.Models;

namespace WeeklyAssignment.Data;

public static class DesignationSeedData
{
    public static List<Designation> GetDesignations()
    {
        return
        [
            new()
            {
                Id = 1,
                Name = "Software Engineer"
            },

            new()
            {
                Id = 2,
                Name = "QA Engineer"
            },

            new()
            {
                Id = 3,
                Name = "Project Manager"
            },

            new()
            {
                Id = 4,
                Name = "UI/UX Designer"
            },

            new()
            {
                Id = 5,
                Name = "DevOps Engineer"
            }
        ];
    }
}