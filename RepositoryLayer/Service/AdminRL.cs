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
    public class AdminRL : IAdminRL
    {
        public IConfiguration configuration { get; }

        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        SqlConnection sqlConnection;

        public AdminModel Login(AdminLoginModel loginModel)
        {
            AdminModel adminModel = new AdminModel();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand("dbo.AdminLogin", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailId", loginModel.AdminEmail);
                    command.Parameters.AddWithValue("@Password", loginModel.AdminPassword);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            adminModel.AdminPassword = Convert.ToString(reader["AdminPassword"] == DBNull.Value ? default : reader["AdminPassword"]);
                            adminModel.AdminEmail = Convert.ToString(reader["AdminEmail"] == DBNull.Value ? default : reader["AdminEmail"]);

                            if ( adminModel.AdminPassword == loginModel.AdminPassword)
                            {
                                return adminModel;
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                sqlConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return default;
        }
    }
}
