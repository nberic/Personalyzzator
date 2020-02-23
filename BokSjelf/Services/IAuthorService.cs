using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BokSjelf.Models;

namespace BokSjelf.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorAsync(Guid authorId);
        Task<Author> CreateAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(Guid authorId, Author author);
        Task<Author> DeleteAuthorAsync(Guid authorId);
    }
}