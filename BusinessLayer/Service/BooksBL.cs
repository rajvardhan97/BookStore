using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BooksBL : IBooksBL
    {
        private IBooksRL booksRL;

        public BooksBL(IBooksRL booksRL)
        {
            this.booksRL = booksRL;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                return this.booksRL.AddBook(bookModel);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public BookModel UpdateBook(BookModel bookModel)
        {
            try
            {
                return this.booksRL.UpdateBook(bookModel);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool DeleteBook(int BookId)
        {
            try
            {
                return this.booksRL.DeleteBook(BookId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.booksRL.GetAllBooks();
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
        public BookModel GetBookById(int BookId)
        {
            try
            {
                return this.booksRL.GetBookById(BookId);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
    }
}
