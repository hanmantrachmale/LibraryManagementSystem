using LibraryManagementSystem.Factories;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Add Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_bookRepository.GetAllBooks().Any(b => b.ISBN == book.ISBN))
            {
                return Conflict("A Book with same ISBN already exists.");
            }
            var newBook = BookFactory.CreateBook(book.Title, book.Author, book.ISBN);
            _bookRepository.AddBook(newBook);
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        /// <summary>
        /// Borrow Book
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpPut("{id}/borrow")]
        public IActionResult BorrowBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            if (book.IsBorrowed)
            {
                return Conflict("The book is already borrowed.");
            }
            _bookRepository.BorrowBook(id);
            return NoContent();
        }

        /// <summary>
        /// Return Book
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpPut("{id}/return")]
        public IActionResult ReturnBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            if (!book.IsBorrowed)
            {
                return Conflict("The book is not currently borrowed.");
            }
            _bookRepository.ReturnBook(id);
            return NoContent();
        }

        /// <summary>
        /// Get All Books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListBooks()
        {
            var books = _bookRepository.GetAllBooks();
            if (books == null || !books.Any()) { return NoContent(); }

            return Ok(books);
        }

        /// <summary>
        /// Get Book By Id
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(book);
        }

    }
}
