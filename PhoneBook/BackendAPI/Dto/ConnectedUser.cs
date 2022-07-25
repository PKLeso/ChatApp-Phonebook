namespace PhoneBook.Dto
{
    public class ConnectedUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ConnectionId { get; set; }

        public ConnectedUser(Guid newId, string newName, string newConnId)
        {
            Id = newId;
            Name = newName;
            ConnectionId = newConnId;
        }
    }
}
