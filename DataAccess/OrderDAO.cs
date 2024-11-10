using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        FStoreDBAssignmentContext context = new FStoreDBAssignmentContext();
        IMemberRepository MemberRepository = new MemberRepository();
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();



        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public void DeleteOrder(int orderId)
        {
            if (GetOrderById(orderId) != null)
            {
                context.Orders.Remove(GetOrderById(orderId));
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid Id");
            }
        }


        public List<Order> GetOrdersByMemberId(int memberId)
        {
            List<Order>? orders = context.Orders.Where(o=> o.MemberId == memberId).ToList();
            return orders;
        }

        public void InsertOrder(Order order)
        {
            if(!order.ShippedDate.HasValue || !order.RequiredDate.HasValue)
            {
                if (GetOrderById(order.OrderId) == null && MemberRepository.GetMemberByID((int)order.MemberId) != null)
                {
                    if(order.MemberId != null){
                        MemberRepository.GetMemberByID(order.MemberId.Value).Orders.Add(order);
                        //order.Member = new Member();
                    }
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Try again");
                }
            }
            else if (DateTime.Compare((DateTime)order.RequiredDate, order.OrderDate) > 0 && DateTime.Compare((DateTime)order.ShippedDate, (DateTime)order.RequiredDate) > 0)
            {

                if (GetOrderById(order.OrderId) == null && MemberRepository.GetMemberByID((int)order.MemberId) != null)
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Try again");
                }
            }
            else
            {
                throw new Exception("Try again");
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders.Include(o=>o.Member).ToList();
        }

        public void UpdateOrder(Order order)
        {
            Order order1 = GetOrderById(order.OrderId);
            Member member = context.Members.SingleOrDefault(p => p.MemberId == order.MemberId);
            try
            {
                if (order1 != null)
                {
                    order1.Freight = order.Freight;
                    order1.MemberId = order.MemberId;
                    order1.OrderDate = order.OrderDate;
                    order1.RequiredDate = order.RequiredDate;
                    order1.ShippedDate = order.ShippedDate;
                    order1.Member = member;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order GetOrderById(int orderId)
        {
            var orderlist = context.Orders.ToList();
            Order order = context.Orders.SingleOrDefault(p => p.OrderId == orderId);
            return order;
        }

    }
}
