using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        FStoreDBAssignmentContext context = new FStoreDBAssignmentContext();
        IMemberRepository MemberRepository = new MemberRepository();
        IOrderRepository OrderRepository = new OrderRepository();

        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            var orderdetailList = new List<OrderDetail>();
            try
            {
                orderdetailList = context.OrderDetails.Include(p=>p.Product).Where(o=>o.OrderId==orderId).ToList();
                if (orderdetailList == null) orderdetailList = new List<OrderDetail>();
            }
            catch (Exception)
            {

                throw;
            }
            return orderdetailList;
        }

        public OrderDetail GetOrderDetail(int orderId, int productid)
        {
            var orderdetail = context.OrderDetails.Where(o=>o.OrderId == orderId && o.ProductId==productid).SingleOrDefault();
                //(from a in context.OrderDetails where a.OrderId == orderId && a.ProductId == productid select a).SingleOrDefault();
            return orderdetail;
        }

        public void DeleteOrderDetail(int orderId, int productid)
        {
            if (GetOrderDetail(orderId, productid) != null)
            {
                context.OrderDetails.Remove(GetOrderDetail(orderId, productid));
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid Id");
            }
        }

        public void Insert(OrderDetail orderDetail)
        {
            if (GetOrderDetail(orderDetail.OrderId, orderDetail.ProductId) == null)
            {
                context.OrderDetails.Add(orderDetail);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Try again");
            }
        }

        public void Update(OrderDetail orderdetail)
        {
            OrderDetail order1 = GetOrderDetail(orderdetail.OrderId, orderdetail.ProductId);

            try
            {
                if (order1 != null)
                {
                    order1.ProductId = orderdetail.ProductId;
                    order1.Quantity = orderdetail.Quantity;
                    order1.Discount = orderdetail.Discount;
                    order1.UnitPrice = orderdetail.UnitPrice;


                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
