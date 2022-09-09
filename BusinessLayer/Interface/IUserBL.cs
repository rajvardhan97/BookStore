using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public bool Register(UserRegisterModel userRegister);
        public string Login(LoginModel loginModel);
        public string ForgotPassword(string emailId);
        public string ResetPassword(ResetModel resetModel);
    }
}
