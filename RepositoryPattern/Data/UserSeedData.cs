using RepositoryPattern.Models;

namespace RepositoryPattern.Data;

public static class UserSeedData
{
    public static List<User> GetUsers()
    {
        Random random = new();

        string[] firstNames =
        {
            "Ali", "Ahmed", "Usman", "Hamza", "Bilal",
            "Ayesha", "Fatima", "Sara", "Hina", "Zain",
            "Hassan", "Talha", "Saad", "Noor", "Iqra",
            "Maham", "Danish", "Anum", "Maryam", "Abdullah"
        };

        string[] lastNames =
        {
            "Khan", "Ahmed", "Malik", "Butt", "Qureshi",
            "Sheikh", "Raza", "Javed", "Mirza", "Chaudhry"
        };

        List<User> users = new();

        for (int i = 1; i <= 20; i++)
        {
            string firstName =
                firstNames[random.Next(firstNames.Length)];

            string lastName =
                lastNames[random.Next(lastNames.Length)];

            users.Add(new User
            {
                Id = i,

                FirstName = firstName,

                LastName = lastName,

                Email =
                    $"{firstName.ToLower()}.{lastName.ToLower()}{i}@example.com",

                PhoneNumber =
                    $"03{random.Next(10, 100)}-{random.Next(1000000, 10000000)}",

                DateOfBirth =
                    DateTime.Today
                        .AddYears(-random.Next(20, 50))
                        .AddDays(-random.Next(365)),

                IsActive = random.Next(2) == 1
            });
        }

        return users;
    }
}