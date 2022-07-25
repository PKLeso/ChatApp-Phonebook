namespace PhoneBook.Models
{
    public class Connection : BaseEntity
    {
        public string SignalRId { get; set; }

        public DateTime TimeStamp { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
