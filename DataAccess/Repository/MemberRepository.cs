using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(int memberID) => MemberDAO.Instance.DeleteMember(memberID);


        public Member GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);



        public Member GetMemberByID(int memberID) => MemberDAO.Instance.GetMemberByID(memberID);



        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.Members();



        public void InsertMember(Member member) => MemberDAO.Instance.InsertMember(member);


        public void UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);

    }
}
