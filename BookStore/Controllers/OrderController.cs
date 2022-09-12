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
    public class OrderController : Controller
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost("PlaceOrder")]
        public IActionResult AddOrder(OrderModel order)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.orderBL.AddOrder(order, UserId);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Order placed Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Order failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder(int OrderId)
        {
            try
            {
                var result = this.orderBL.DeleteOrder(OrderId);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Order Deleted Successfully",});
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Deletion Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("ViewOrder")]
        public IActionResult ViewOrder()
        {
            try
            {
                var result = this.orderBL.ViewOrder();
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Displaying All Orders", Data = result });
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
