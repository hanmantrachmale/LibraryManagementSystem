using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Factories
{
    public class BookFactory
    {
        private static int _nextId = 1;
        public static Book CreateBook(string title, string author, string isbn)
        {
            return new Book { Id = ++_nextId, Title = title, Author = author, ISBN = isbn, IsBorrowed = false };
        }

    }
}
