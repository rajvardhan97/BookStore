using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrderBL : IOrderBL
    {
        private IOrderRL orderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public bool AddOrder(OrderModel order, int UserId)
        {
            try
            {
                return this.orderRL.AddOrder(order, UserId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool DeleteOrder(int OrderId)
        {
            try
            {
                return this.orderRL.DeleteOrder(OrderId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public List<OrderModel> ViewOrder()
        {
            try
            {
                return this.orderRL.ViewOrder();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
