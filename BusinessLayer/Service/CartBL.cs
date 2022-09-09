using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL : ICartBL
    {
        private ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public AddCartModel AddtoCart(AddCartModel addCartModel, int UserId)
        {
            try
            {
                return this.cartRL.AddtoCart(addCartModel, UserId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool UpdateCart(int CartId, int UserId, int BooksinCart)
        {
            try
            {
                return this.cartRL.UpdateCart(CartId, UserId, BooksinCart);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool DeleteCart(int CartId, int UserId)
        {
            try
            {
                return this.cartRL.DeleteCart(CartId, UserId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public IEnumerable<AddCartModel> GetAllCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllCart(UserId);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
    }
}
