﻿using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Services.Exception;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Services
{
    public class BookService
    {
        private readonly BookstoreContext _context;

        public BookService(BookstoreContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> FindAllAsync()
        {
            return await _context.Books.Include(x => x.Genres).ToListAsync();
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await _context.Books.Include(x => x.Genres).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book book)
        {
            bool hasAny = await _context.Books.AnyAsync(x => x.Id == book.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
        
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Books.FindAsync(id);
                _context.Books.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
    }
}
