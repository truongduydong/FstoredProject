using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails(int OrderId);
        OrderDetail GetOrderDetail(int OrderId, int productID);
        void Update(OrderDetail orderDetail);
        void Delete(int orderid, int productid);
        void Add(OrderDetail orderDetail);
    }
}
