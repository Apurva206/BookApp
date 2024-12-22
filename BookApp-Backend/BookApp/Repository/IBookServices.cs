using BookApp.Model;

namespace BookApp.Repository
{
    public interface IBookServices
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByISBNAsync(string isbn);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(string isbn);
    }
}
