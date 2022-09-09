using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public bool Register(UserRegisterModel userRegister)
        {
            try
            {
                return this.userRL.Register(userRegister);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public string Login(LoginModel loginModel)
        {
            try
            {
                return this.userRL.Login(loginModel);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public string ForgotPassword(string emailId)
        {
            try
            {
                return this.userRL.ForgotPassword(emailId);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public string ResetPassword(ResetModel resetModel)
        {
            try
            {
                return this.userRL.ResetPassword(resetModel);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
    }
}
