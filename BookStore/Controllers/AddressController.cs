using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddressModel address)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.addressBL.AddAddress(address, UserId);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Address added Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Address not added" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel address)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.addressBL.UpdateAddress(address, UserId);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Address Updated Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Address not updated" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetAllAddress()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.addressBL.GetAllAddress(UserId);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Displaying All Address", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Display failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
