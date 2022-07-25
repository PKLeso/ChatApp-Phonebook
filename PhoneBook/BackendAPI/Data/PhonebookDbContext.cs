using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Data
{
    public class PhonebookDbContext: DbContext
    {
        public PhonebookDbContext(DbContextOptions<PhonebookDbContext> options) : base(options){ }

        public DbSet<Phonebook> Phonebooks { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }


    }
}
