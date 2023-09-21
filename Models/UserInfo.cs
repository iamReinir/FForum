namespace forum.Models
{
    public class UserInfo
    {
        public UserInfo(string displayName) {
            Name = displayName;
        }
        public int? ID { init; get; }
        public string Name { set; get; }
        public string? Email { set; get; }
        public DateOnly? Birthdate { set; get; }
        
        public string? Telephone { set; get; }
        public string? Address { set; get; }

        public DateTime Last_update { set; get; }

        private User? owner;
        public User? User { init { owner = value; ID = owner?.ID ; } get { return owner; } }

    }
}
