using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserRegisterModel userRegisterModel)
        {
            try
            {
                var result = this.userBL.Register(userRegisterModel);
                if(result != null)
                {
                    return this.Ok(new { Success = true, Message = "User Registration Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "Registration failed" });
                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = this.userBL.Login(loginModel);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Login Successfull", Data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Forgotpassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = this.userBL.ForgotPassword(email);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Token sent successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Invalid Credentials" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetModel resetModel)
        {
            try
            {
                var result = this.userBL.ResetPassword(resetModel);
                if(result != null)
                {
                    return Ok(new { Success = true, Message = " Password reset succcessful" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Password reset unsuccessful"});
                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }
    }
}
