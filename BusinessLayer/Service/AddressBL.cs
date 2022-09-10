using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AddressBL : IAddressBL
    {
        private IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public bool AddAddress(AddressModel address, int UserId)
        {
            try
            {
                return this.addressRL.AddAddress(address, UserId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool UpdateAddress(AddressModel address, int UserId)
        {
            try
            {
                return this.addressRL.UpdateAddress(address, UserId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public IEnumerable<AddressModel> GetAllAddress(int UserId)
        {
            try
            {
                return this.addressRL.GetAllAddress(UserId);
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
    }
}
