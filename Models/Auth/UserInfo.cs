namespace ProductWebAPI.Models
{
    public class UserInfo
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Валерій",
                LastName = "Вітрієнко",
                Email = "valerka@example.com",
                DateOfBirth = new DateTime(1950, 11, 11),
                Password = "qwedsf2004",
                LastLoginDate = new DateTime(2024, 4, 27),
                FailedLoginAttempts = 5
            },
        };
    }
}
