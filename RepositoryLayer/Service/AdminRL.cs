using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public string Login(AdminLoginModel loginModel)
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

                    command.Parameters.AddWithValue("@AdminEmail", loginModel.AdminEmail);
                    command.Parameters.AddWithValue("@AdminPassword", loginModel.AdminPassword);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            adminModel.AdminPassword = Convert.ToString(reader["AdminPassword"] == DBNull.Value ? default : reader["AdminPassword"]);
                            adminModel.AdminEmail = Convert.ToString(reader["AdminEmail"] == DBNull.Value ? default : reader["AdminEmail"]);

                            if ( adminModel.AdminPassword == loginModel.AdminPassword)
                            {
                                return GenerateSecurityToken(adminModel.AdminEmail, adminModel.AdminId);
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

        public string GenerateSecurityToken(string AdminEmail, long AdminId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration[("JWT:key")]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("AdminEmail", AdminEmail),
                    new Claim("AdminId", AdminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
