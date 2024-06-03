using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public bool BorrowBook(int id)
        {
            var book = GetBookById(id);
            if (book != null && !book.IsBorrowed)
            {
                book.IsBorrowed = true;
                return true;
            }
            return false;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id)!;
        }

        public bool ReturnBook(int id)
        {
            var book = GetBookById(id);
            if (book != null && book.IsBorrowed)
            {
                book.IsBorrowed = false;
                return true;
            }
            return false;
        }
    }
}
