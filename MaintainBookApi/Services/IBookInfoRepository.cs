using MaintainBookApi.Entities;

namespace MaintainBookApi.Services
{
    public interface IBookInfoRepository
    {

        Task<IEnumerable<Book>> GetBooksAsync();

        Task<Book?> GetBookAsync(int bookId);

        Task AddBookAsync(Book book);

        Task<bool> SaveChangesAsync();
    }
}
