using MaintainBookApi.DbContexts;
using MaintainBookApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaintainBookApi.Services
{
    public class BookInfoRepository : IBookInfoRepository
    {
        private readonly BookInfoContext _bookInfoContext;

        public BookInfoRepository(BookInfoContext bookInfoContext)
        {
            _bookInfoContext = bookInfoContext ?? throw new ArgumentNullException(nameof(bookInfoContext));
        }
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookInfoContext.Books.OrderBy(b => b.Id).ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int bookId)
        {
            return await _bookInfoContext.Books.Where(b => b.Id == bookId).FirstOrDefaultAsync();
        }
        public async Task AddBookAsync(Book book)
        {
             await _bookInfoContext.Books.AddAsync(book);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _bookInfoContext.SaveChangesAsync() > 0;
        }

    }
}
