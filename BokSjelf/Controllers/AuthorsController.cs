using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BokSjelf.Models;
using BokSjelf.Services;

namespace BokSjelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IAuthorService _authorService;
        public AuthorsController(ILogger<AuthorsController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            _logger.LogInformation($"{ new DateTime() } -> GET: Requested all Authors.");
            _logger.LogInformation($"{ new DateTime() } -> GET: Returned - Requested all Authors.");
            return Ok(await _authorService.GetAllAuthorsAsync());
        }

        [HttpGet("{authorId}")]
        public async Task<ActionResult<Author>> GetAuthor(Guid authorId)
        {
            _logger.LogInformation($"{ new DateTime() } -> GET: Requested the Author with GUID { authorId }.");
            var requestedAuthor = await _authorService.GetAuthorAsync(authorId);
            if (requestedAuthor == null)
            {
                _logger.LogInformation($"{ new DateTime() } -> GET: Requested Author with GUID { authorId } not found.");
                return NotFound($"No such Author: { authorId }");
            }
            _logger.LogInformation($"{ new DateTime() } -> GET: Returned - Requested the Author with GUID { authorId }.");
            return Ok(requestedAuthor);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor([ FromBody ] Author author)
        {
            _logger.LogInformation($"{ new DateTime() } -> POST: Requested new Author creation.");
            var createdAuthor = await _authorService.CreateAuthorAsync(author);
            _logger.LogInformation($"{ new DateTime() } -> POST: Requested Author is created with GUID { createdAuthor.AuthorId }.");
            _logger.LogInformation($"{ new DateTime() } -> POST: Returned - Requested new Author creation.");
            return Ok(createdAuthor);
        }

        [HttpPut("{authorId}")]
        public async Task<ActionResult<Author>> UpdateAuthor(Guid authorId, [ FromBody ] Author author)
        {
            _logger.LogInformation($"{ new DateTime() } -> PUT: Requested update of Author with GUID { authorId }.");
            if (authorId != author.AuthorId)
            {
                var message = $"Author ID inconsistency found. Two GUIDs provided: { authorId } and { author.AuthorId }.";
                _logger.LogInformation($"{ new DateTime() } -> { message }");
                return BadRequest($"{ message }");
            }
            var updatedAuthor = await _authorService.UpdateAuthorAsync(authorId, author);
            if (updatedAuthor == null)
            {
                _logger.LogInformation($"{ new DateTime() } -> PUT: Requested update of Author with GUID { authorId } which is not found.");
                return NotFound($"No such Author: { authorId }");
            }
             _logger.LogInformation($"{ new DateTime() } -> PUT: Returned - Requested update of Author with GUID { authorId }.");
            return Ok(updatedAuthor);
        }

        [HttpDelete("{authorId}")]
        public async Task<ActionResult<Author>> DeleteAuthor(Guid authorId)
        {
            _logger.LogInformation($"{ new DateTime() } -> DELETE: Requested deletion of Author with GUID { authorId }.");
            var deletedAuthor = await _authorService.DeleteAuthorAsync(authorId);
            if (deletedAuthor == null)
            {
                _logger.LogInformation($"{ new DateTime() } -> DELETE: Requested deletion of Author with GUID { authorId } which is not found.");
                return NotFound($"No such Author: { authorId }");
            }
            _logger.LogInformation($"{ new DateTime() } -> DELETE: Returned - Requested deletion of Author with GUID { authorId }.");
            return deletedAuthor;
        }
    }
}