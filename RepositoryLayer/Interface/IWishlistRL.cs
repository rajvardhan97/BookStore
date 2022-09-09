using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public bool AddtoWishlist(int BookId, int UserId);
        public bool DeletefromWishlist(int WishlistId);
        public IEnumerable<WishlistModel> GetWishlist(int UserId);
    }
}
