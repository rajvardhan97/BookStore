using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public AddCartModel AddtoCart(AddCartModel addCartModel, int UserId);
        public bool UpdateCart(int CartId, int UserId, int BooksinCart);
        public bool DeleteCart(int CartId, int UserId);
        public IEnumerable<AddCartModel> GetAllCart(int UserId);
    }
}
