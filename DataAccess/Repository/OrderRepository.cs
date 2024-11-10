using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(int orderId) => OrderDAO.Instance.DeleteOrder(orderId);

        public Order GetOrderById(int orderId) => OrderDAO.Instance.GetOrderById(orderId);


        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrders();


        public List<Order> GetOrdersByMemberId(int memberId) => OrderDAO.Instance.GetOrdersByMemberId(memberId);


        public void InsertOrder(Order order) => OrderDAO.Instance.InsertOrder(order);


        public void UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);


    }
}
