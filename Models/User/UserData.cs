namespace RESTwebAPI.Models
{
    public class UserData
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Дмитро",
                LastName = "Захаров",
                Email = "dmitro@example.com",
                DateOfBirth = new DateTime(2004, 09, 26),
                Password = "zaharody463",
                LastLoginDate = DateTime.Now.AddDays(-1),
                FailedLoginAttempts = 0
            },
            new User
            {
                Id = 2,
                FirstName = "Наталія",
                LastName = "Дунаєва",
                Email = "natali@example.com",
                DateOfBirth = new DateTime(2002, 12, 20),
                Password = "natalia7942n",
                LastLoginDate = DateTime.Now.AddDays(-3),
                FailedLoginAttempts = 2
            },
            new User
            {
                Id = 3,
                FirstName = "Назарій",
                LastName = "Кіріяк",
                Email = "nazarii@example.com",
                DateOfBirth = new DateTime(2004, 03, 24),
                Password = "password5371",
                LastLoginDate = new DateTime(2024,3,26),
                FailedLoginAttempts = 50
            },
        };
    }
}
