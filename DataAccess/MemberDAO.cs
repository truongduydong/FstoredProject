using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        FStoreDBAssignmentContext context = new FStoreDBAssignmentContext();


        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();



        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Member> Members()
        {

            var memberList = context.Members.ToList();

            return memberList;
        }

        public List<Member> GetMemberList()
        {
            return context.Members.ToList();
        }

        public void DeleteMember(int memberId)
        {
            if (GetMemberByID(memberId) != null)
            {
                context.Remove(context.Members.First<Member>(p => p.MemberId == memberId));
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid ID");
            }
        }

        public Member GetMemberByEmail(string email)
        {
            Member member = context.Members.SingleOrDefault(p => p.Email.Equals(email));
            return member;
        }

        public Member GetMemberByID(int memberid)
        {
            Member member = context.Members.Include(p=>p.Orders).SingleOrDefault(p => p.MemberId == memberid);
            return member;
        }

        public void InsertMember(Member member)
        {
            if (GetMemberByID(member.MemberId) == null && GetMemberByEmail(member.Email) == null)
            {
                member.Orders = new List<Order>();
                context.Members.Add(member);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Member ID/Email is already exists.");

            }
        }

        public void UpdateMember(Member member)
        {
            var mem = context.Members.Where(m=>m.MemberId==member.MemberId).SingleOrDefault();
            List<Order> orders = context.Orders.Where(o=>o.MemberId==member.MemberId).ToList();
            if (mem != null)
            {
                mem.CompanyName = member.CompanyName;
                mem.Email = member.Email;
                mem.Country = member.Country;
                mem.City = member.City;
                mem.Password = member.Password;
                mem.Orders = orders;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Member does not exitst");
            }
        }


    }
}
