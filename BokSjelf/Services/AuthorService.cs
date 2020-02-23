using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BokSjelf.Models;

namespace BokSjelf.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BokSjelfContext _context;
        public AuthorService(BokSjelfContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> GetAuthorAsync(Guid authorId)
        {
            var requestedAuthor = await _context.Authors.FirstOrDefaultAsync(author => author.AuthorId == authorId);
            if (requestedAuthor == null)
            {
                return null;
            }
            return requestedAuthor;
        }
        public async Task<Author> CreateAuthorAsync(Author author)
        {
            author.AuthorId = Guid.NewGuid();
            _context.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }
        public async Task<Author> UpdateAuthorAsync(Guid authorId, Author author)
        {
            if (!AuthorExists(authorId))
            {
                return null;
            }

            _context.Entry(author).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ArgumentException($"Could not update user { authorId }. Try again later.");
            }
            return author;
        }
        public async Task<Author> DeleteAuthorAsync(Guid authorId)
        {
            var requestedAuthor = await _context.Authors.FirstOrDefaultAsync(author => author.AuthorId == authorId);
            if (requestedAuthor == null)
            {
                return null;
            }
            _context.Authors.Remove(requestedAuthor);
            await _context.SaveChangesAsync();
            return requestedAuthor;
        }

        private bool AuthorExists(Guid authorId)
        {
            return _context.Authors.Any(author => author.AuthorId == authorId);
        }
    }
}