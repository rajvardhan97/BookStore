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
    public class AddressRL : IAddressRL
    {
        public IConfiguration configuration { get; }
        SqlConnection sqlConnection;
        public AddressRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddAddress(AddressModel address, int UserId)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.AddAddress", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                    sqlCommand.Parameters.AddWithValue("@City", address.City);
                    sqlCommand.Parameters.AddWithValue("@State", address.State);
                    sqlCommand.Parameters.AddWithValue("@TypeId", address.TypeId);
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

        public bool UpdateAddress(AddressModel address, int UserId)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateAddress", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@AddressId", address.AddressId);
                    sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                    sqlCommand.Parameters.AddWithValue("@City", address.City);
                    sqlCommand.Parameters.AddWithValue("@State", address.State);
                    sqlCommand.Parameters.AddWithValue("@TypeId", address.TypeId);
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

        public IEnumerable<AddressModel> GetAllAddress(int UserId)
        {
            List<AddressModel> address = new List<AddressModel>();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    //SqlCommand sqlCommand = new SqlCommand("dbo.GetAddress", sqlConnection);
                    String query = "SELECT AddressId, Address, City, State, TypeId FROM Address WHERE UserId = '" + UserId + "'";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            address.Add(new AddressModel()
                            {
                                AddressId = (int)sqlDataReader["AddressId"],
                                Address = sqlDataReader["Address"].ToString(),
                                City = sqlDataReader["City"].ToString(),
                                State = sqlDataReader["State"].ToString(),
                                TypeId = (int)sqlDataReader["TypeId"]
                            });
                        }
                        return address;
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
