using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }
        public bool AddtoWishlist(int BookId, int UserId)
        {
            try
            {
                return this.wishlistRL.AddtoWishlist(BookId, UserId);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public bool DeletefromWishlist(int WishlistId)
        {
            try
            {
                return this.wishlistRL.DeletefromWishlist(WishlistId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public IEnumerable<WishlistModel> GetWishlist(int UserId)
        {
            try
            {
                return this.wishlistRL.GetWishlist(UserId);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
    }
}
