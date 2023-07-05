namespace BowlingAlleyAPI.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
