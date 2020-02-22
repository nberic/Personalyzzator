using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JsonParsingTest.Models
{
    public class PersonContext: DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
    }
}