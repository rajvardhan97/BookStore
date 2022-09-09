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
using System.Configuration;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        public IConfiguration configuration { get; }

        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        SqlConnection sqlConnection;

        public bool Register(UserRegisterModel userRegister)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand("dbo.Register", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    var encryptPassword = EncryptPassword(userRegister.Password);
                    sqlCommand.Parameters.AddWithValue("@FullName", userRegister.FullName);
                    sqlCommand.Parameters.AddWithValue("@EmailId", userRegister.EmailId);
                    sqlCommand.Parameters.AddWithValue("@Password", encryptPassword);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", userRegister.MobileNumber);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
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

        public string Login(LoginModel loginModel)
        {
            UserRegisterModel registerModel = new UserRegisterModel();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand("dbo.Login", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    var encrpt = EncryptPassword(loginModel.Password);
                    command.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                    command.Parameters.AddWithValue("@Password", encrpt);

                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            registerModel.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                            registerModel.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                            registerModel.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);

                            if (encrpt == registerModel.Password)
                            {
                                var token = GenerateSecurityToken(registerModel.EmailId, registerModel.UserId);
                                return token;
                            }
                        }
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return default;
        }

        public static string EncryptPassword(string password)
        {
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(passwordBytes);
        }

        public static string DecryptPassword(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string GenerateSecurityToken(string EmailId, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration[("JWT:key")]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, EmailId),
                    new Claim("UserId", UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string ForgotPassword(string emailId)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand("dbo.ForgetPassword", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailId", emailId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var userId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                            var token = GenerateSecurityToken(emailId, userId);
                            MSMQModel msmqModel = new MSMQModel();
                            msmqModel.sendData2Queue(token);
                            return token;
                        }
                    }
                    else
                    {
                        return null;
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return default;
        }

        public string ResetPassword(ResetModel resetModel)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            try
            {
                using(sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand("dbo.ResetPassword", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    var encryptedPassword = EncryptPassword(resetModel.Password);

                    command.Parameters.AddWithValue("@EmailId", resetModel.EmailId);
                    command.Parameters.AddWithValue("@Password", encryptedPassword);


                    var result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return "Password Changed";
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
        }
    }
}
