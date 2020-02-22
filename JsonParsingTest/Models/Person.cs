using System;

namespace JsonParsingTest.Models
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public int BandNumber { get; set; }
        public DateTime? Birthday { get; set; }
    }
}