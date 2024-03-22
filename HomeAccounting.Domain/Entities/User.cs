using HomeAccounting.Domain.Enums;

namespace HomeAccounting.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
