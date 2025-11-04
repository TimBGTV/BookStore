using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext _context;
        public BooksRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Get()
        {
            var bookEntities = await _context.Books
                .AsNoTracking()
                .ToListAsync();

            var books = bookEntities
                .Select(x => Book.Create(x.Id, x.Title, x.Description, x.Price).Book)
                .ToList();

            return books;
        }

        public async Task<Guid> Create(Book book)
        {
            // Сущность, которую нам нужно занести в базу данных.
            var bookEntity = new BookEntity { Id = book.Id, Title = book.Title, Description = book.Description, Price = book.Price };

            // _context - экземпляр DbContext, представляющий сессию с базой данных, AddAsync() - асинхронно начинает отслеживание сущности как новой (состояние "Added").
            await _context.Books.AddAsync(bookEntity);
            //Фактическая запись в базу данных
            //Entity Framework формирует SQL-команду INSERT и выполняет ее.
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _context.Books.Where(x => x.Id == id).ExecuteUpdateAsync(x => x
            .SetProperty(b => b.Title, b => title)
            .SetProperty(b => b.Description, b => description)
            .SetProperty(b => b.Price, b => price));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Books.Where(x => x.Id == id).ExecuteDeleteAsync();
            return id;
        }
    }
}
