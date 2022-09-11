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
    public class FeedbackRL : IFeedbackRL
    {
        public IConfiguration configuration { get; }
        SqlConnection sqlConnection;
        public FeedbackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddFeedback(FeedbackModel feedback, int UserId)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.AddFeedback", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Rating", feedback.Rating);
                    sqlCommand.Parameters.AddWithValue("@Review", feedback.Review);
                    sqlCommand.Parameters.AddWithValue("@BookId", feedback.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);

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

        public List<FeedbackModel> GetAllFeedback()
        {
            List<FeedbackModel> feedbacks = new List<FeedbackModel>();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.GetFeedback", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            feedbacks.Add(new FeedbackModel()
                            {
                                
                                FeedbackId = (int)sqlDataReader["FeedbackId"],
                                Rating = (int)sqlDataReader["Rating"],
                                Review = sqlDataReader["Review"].ToString(),
                                UserId = (int)sqlDataReader["UserId"],
                                BookId = (int)sqlDataReader["BookId"]
                            });
                        }
                        return feedbacks;
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
    }
}
