using System;

namespace BokSjelf.Models
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public Author BookAuthor { get; set; }
        public string Publisher { get; set; }
        public DateTime? PublishingDate { get; set; }
    }
}