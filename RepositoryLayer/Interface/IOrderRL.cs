using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public bool AddOrder(OrderModel order, int UserId);
        public bool DeleteOrder(int OrderId);
        public List<OrderModel> ViewOrder();
    }
}
