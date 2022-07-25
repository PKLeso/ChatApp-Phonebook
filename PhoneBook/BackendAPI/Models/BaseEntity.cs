using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? EntryDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
