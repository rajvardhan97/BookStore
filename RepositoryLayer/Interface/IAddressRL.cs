using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public bool AddAddress(AddressModel address, int UserId);
        public bool UpdateAddress(AddressModel address, int UserId);
        public IEnumerable<AddressModel> GetAllAddress(int UserId);
    }
}
