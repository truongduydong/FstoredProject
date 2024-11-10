using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        void InsertOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
        Order GetOrderById(int orderId);
        List<Order> GetOrdersByMemberId(int memberId);
    }
}
