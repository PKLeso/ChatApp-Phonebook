using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Models
{
    public class Phonebook : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        List<Entry> Entries { get; set; }

    }

}
