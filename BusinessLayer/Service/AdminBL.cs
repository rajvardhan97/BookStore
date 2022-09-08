using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public string Login(AdminLoginModel loginModel)
        {
            try
            {
                return this.adminRL.Login(loginModel);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
