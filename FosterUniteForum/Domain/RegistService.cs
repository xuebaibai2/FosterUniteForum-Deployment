using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain.Utility;

namespace FosterUniteForum.Domain
{
    public class RegistService : BasicService
    {

        public RegistService() { }

        public RegistService(ForumDbContext context) : base(context) { }

        public void SetMemberGroup(string userID, int groupNameID)
        {
            GroupMember groupMember = new GroupMember();
            //groupMember.GroupId = (int)GROUPNAME.Member;
            groupMember.GroupId = groupNameID;
            groupMember.ForumUserId = new ForumUserManager().GetUserID(userID);
            context.GroupMember.Add(groupMember);
            context.SaveChanges();
        }
        
    }
}
