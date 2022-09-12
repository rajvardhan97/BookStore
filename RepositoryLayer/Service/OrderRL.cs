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
    public class OrderRL : IOrderRL
    {
        public IConfiguration configuration { get; }
        SqlConnection sqlConnection;
        public OrderRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddOrder(OrderModel order, int UserId)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.AddOrder", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@OrderQty", order.OrderQty);
                    sqlCommand.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    sqlCommand.Parameters.AddWithValue("@AddressId", order.AddressId);
                    sqlCommand.Parameters.AddWithValue("@BookId", order.BookId);
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

        public bool DeleteOrder(int OrderId)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.RemoveOrder", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@OrderId", OrderId);
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
        public List<OrderModel> ViewOrder()
        {
            List<OrderModel> order = new List<OrderModel>();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.GetOrder", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            order.Add(new OrderModel()
                            {

                                OrderId = (int)sqlDataReader["OrderId"],
                                BookId = (int)sqlDataReader["BookId"],
                                BookName = sqlDataReader["BookName"].ToString(),
                                BookImage = sqlDataReader["BookImage"].ToString(),
                                AuthorName = sqlDataReader["AuthorName"].ToString(),
                                TotalPrice = (double)sqlDataReader["TotalPrice"],
                                OrderQty = (int)sqlDataReader["OrderQty"],
                                OrderDate = (DateTime)sqlDataReader["OrderDate"],
                                AddressId = (int)sqlDataReader["AddressId"],
                                UserId = (int)sqlDataReader["UserId"]
                            });
                        }
                        return order;
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
