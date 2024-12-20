using BookApp.Data;
using BookApp.Model;

namespace BookApp.Repository
{
    public class BookRepository : IBookServices
    {
        private readonly BookAppDbContext _context;

        public BookRepository(BookAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await Task.FromResult(_context.Books.ToList());
        }

        public async Task<Book> GetBookByISBNAsync(string isbn)
        {
            return await Task.FromResult(_context.Books.FirstOrDefault(b => b.ISBN == isbn));
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(string isbn)
        {
            var book = await GetBookByISBNAsync(isbn);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
