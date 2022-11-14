using AutoMapper;
using MaintainBookApi.Entities;
using MaintainBookApi.Models;
using MaintainBookApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace MaintainBookApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly ILogger<BooksController> _logger;
        private readonly IBookInfoRepository _bookInfoRepository;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger,
            IBookInfoRepository bookInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookInfoRepository = bookInfoRepository ?? throw new ArgumentNullException(nameof(bookInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            try
            {
                var bookEntities = await _bookInfoRepository.GetBooksAsync();
                return Ok(_mapper.Map<IEnumerable<BookDto>>(bookEntities));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    "Exception while getting Books", ex);

                return StatusCode(500, "A problem happend while handling your request in API server end");
            }

        }


        [HttpGet("{bookId}", Name = "GetBookbyId")]
        public async Task<ActionResult<BookDto>> GetBook(int bookId)
        {
            try
            {
                //    _logger.LogInformation($"Get Book Request by id {bookId} ");

                var book = await _bookInfoRepository.GetBookAsync(bookId);

                if (book == null)
                {
                    return NotFound();
                }

                //   _logger.LogInformation($"Get Book Response by id {bookId}", book);

                return Ok(_mapper.Map<BookDto>(book));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting Books with Id {bookId}", ex);

                return StatusCode(500, "A problem happend while handling your request in API server end");
            }

        }

        [HttpPost]

        public async Task<ActionResult<BookDto>> CreateBook(BookForCreationDto book)
        {
            try
            {
               // _logger.LogInformation("Create Book Request ", book);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var booktoCreate = _mapper.Map<Entities.Book>(book);

                await _bookInfoRepository.AddBookAsync(booktoCreate);

                await _bookInfoRepository.SaveChangesAsync();

                var createdBook = _mapper.Map<Models.BookDto>(booktoCreate);

                return CreatedAtRoute("GetBookbyId",
                   new
                   {
                       bookId = createdBook.Id,
                   }, createdBook);

                // _logger.LogInformation("Create Book Response ", createdBook);               

            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while Creating Book with Name {book.Name} and author name {book.AuthorName}", ex);

                return StatusCode(500, "A problem happend while handling your request in API server end");
            }

        }



    }
}
