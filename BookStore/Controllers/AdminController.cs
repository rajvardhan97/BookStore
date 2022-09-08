using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AdminLoginModel loginModel)
        {
            try
            {
                var result = this.adminBL.Login(loginModel);
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
    }
}
