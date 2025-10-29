using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BookRepository
    {
        private readonly BookStoreDbContext _context;
        public BookRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Get()
        {
            var bookEntities = await _context.Books.AsNoTracking().ToListAsync();
            var books = bookEntities.Select(x => Book.Create(x.Id, x.Title, x.Description, x.Price).Book).ToList();
            return books;
        }
    }
}
