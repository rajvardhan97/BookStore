using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksBL booksBL;
        public BooksController(IBooksBL booksBL)
        {
            this.booksBL = booksBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = this.booksBL.AddBook(bookModel);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Book added Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Book Addition Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("Updatebook")]
        public IActionResult UpdateBook(BookModel bookModel)
        {
            try
            {
                var result = booksBL.UpdateBook(bookModel);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Book Updated Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Failed to Update Book" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.booksBL.DeleteBook(BookId);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Book Deleted Successfully", Data = result });
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

        [Authorize]
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.booksBL.GetAllBooks();
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Retrieve Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Retrieve Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetBookByID")]
        public IActionResult GetBookById(int BookId)
        {
            try
            {
                var result = this.booksBL.GetBookById(BookId);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Displaying Book by ID Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Failed to Display " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
