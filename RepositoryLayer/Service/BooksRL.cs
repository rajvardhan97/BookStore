using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class BooksRL : IBooksRL
    {
        public IConfiguration configuration { get; }

        public BooksRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        SqlConnection sqlConnection;

        public BookModel AddBook(BookModel bookModel)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.AddBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@Description", bookModel.Description);
                    sqlCommand.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    sqlCommand.Parameters.AddWithValue("@TotalRating", bookModel.TotalRating);
                    sqlCommand.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookModel.BookImage);

                    var result = sqlCommand.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public BookModel UpdateBook(BookModel bookModel)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookId", bookModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@Description", bookModel.Description);
                    sqlCommand.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    sqlCommand.Parameters.AddWithValue("@TotalRating", bookModel.TotalRating);
                    sqlCommand.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookModel.BookImage);

                    var result = sqlCommand.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteBook(int BookId)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.DeleteBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                    var result = sqlCommand.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<BookModel> GetAllBooks()
        {
            BookModel bookModel = new BookModel();
            List<BookModel> books = new List<BookModel>();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.GetAllBooks", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            BookModel model = GetBookDetails(bookModel, sqlDataReader);
                            books.Add(model);
                        }
                        return books;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public BookModel GetBookById(int BookId)
        {
            BookModel bookModel = new BookModel();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.GetBookById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            bookModel = GetBookDetails(bookModel, sqlDataReader);
                        }
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static BookModel GetBookDetails(BookModel bookModel, SqlDataReader sqlDataReader)
        {
            bookModel.BookId = Convert.ToInt32(sqlDataReader["BookId"] == DBNull.Value ? default : sqlDataReader["BookId"]);
            bookModel.BookName = Convert.ToString(sqlDataReader["BookName"] == DBNull.Value ? default : sqlDataReader["BookName"]);
            bookModel.AuthorName = Convert.ToString(sqlDataReader["AuthorName"] == DBNull.Value ? default : sqlDataReader["AuthorName"]);
            bookModel.Description = Convert.ToString(sqlDataReader["Description"] == DBNull.Value ? default : sqlDataReader["Description"]);
            bookModel.Quantity = Convert.ToInt32(sqlDataReader["Quantity"] == DBNull.Value ? default : sqlDataReader["Quantity"]);
            bookModel.TotalRating = Convert.ToInt64(sqlDataReader["TotalRating"] == DBNull.Value ? default : sqlDataReader["TotalRating"]);
            bookModel.Rating = Convert.ToInt64(sqlDataReader["Rating"] == DBNull.Value ? default : sqlDataReader["Rating"]);
            bookModel.OriginalPrice = Convert.ToInt32(sqlDataReader["OriginalPrice"] == DBNull.Value ? default : sqlDataReader["OriginalPrice"]);
            bookModel.DiscountPrice = Convert.ToInt32(sqlDataReader["DiscountPrice"] == DBNull.Value ? default : sqlDataReader["DiscountPrice"]);
            bookModel.BookImage = Convert.ToString(sqlDataReader["BookImage"] == DBNull.Value ? default : sqlDataReader["BookImage"]);

            return bookModel;
        }
    }
}
