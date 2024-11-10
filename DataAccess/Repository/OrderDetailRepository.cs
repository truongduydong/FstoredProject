using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void Add(OrderDetail orderDetail) => OrderDetailDAO.Instance.Insert(orderDetail);

        public void Delete(int orderid, int productid) => OrderDetailDAO.Instance.DeleteOrderDetail(orderid, productid);

        public OrderDetail GetOrderDetail(int OrderId, int productID) => OrderDetailDAO.Instance.GetOrderDetail(OrderId, productID);

        public IEnumerable<OrderDetail> GetOrderDetails(int OrderId) => OrderDetailDAO.Instance.GetOrderDetails(OrderId);

        public void Update(OrderDetail orderDetail) => OrderDetailDAO.Instance.Update(orderDetail);


    }
}
