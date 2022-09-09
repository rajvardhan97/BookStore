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
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost("AddtoCart")]
        public IActionResult AddtoCart(AddCartModel addCartModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault( u => u.Type == "UserId").Value);
                var result = cartBL.AddtoCart(addCartModel, UserId);

                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Book Added to Cart", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Failed to Add Book " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateCart(int CartId, int UserId, int BooksinCart)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = cartBL.UpdateCart(CartId, UserId, BooksinCart);

                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Cart Updated"});
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Failed to Update Cart " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteCart(int CartId, int UserId)
        {
            try
            {
                var result = cartBL.DeleteCart(CartId, UserId);

                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Cart Deleted" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Failed to Delete Cart " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAllCart")]
        public IActionResult GetAllCart(int UserId)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault( g => g.Type == "UserId").Value);
                var result = cartBL.GetAllCart(UserId);

                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Successful ", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Unsuccessful " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
