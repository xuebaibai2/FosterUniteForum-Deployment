using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class GroupMemberService : BasicService
    {
        private GroupMember groupMember;
        private List<GroupMember> groupMemberList;

        public GroupMemberService()
        {
            groupMember = new GroupMember();
            groupMemberList = new List<GroupMember>();
        }
        public GroupMemberService(ForumDbContext context) : base(context) { }

        public bool AddGroupMember(int groupID, int userID)
        {
            GroupMember tempGroupMember = this.GetGroupMember(groupID, userID);
            if (tempGroupMember == null)
            {
                this.groupMember.GroupId = groupID;
                this.groupMember.ForumUserId = userID;
                context.GroupMember.Add(this.groupMember);
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public GroupMember GetGroupMember(int groupID, int userID)
        {
            return context.GroupMember
                .Where(m => m.GroupId == groupID)
                .Where(m => m.ForumUserId == userID)
                .SingleOrDefault();
        }

        public GroupMember GetGroupMemberByUserID(int userID)
        {
            return context.GroupMember
                .Where(m => m.ForumUserId == userID)
                .First();
        }

        public List<GroupMember> GetUserGroup(int userID)
        {
            return context.GroupMember
                .Where(m => m.ForumUserId == userID)
                .ToList();
        }

        public void DeleteGroupMemberByUserID(int userID)
        {
            context.GroupMember.RemoveRange(context.GroupMember.Where(m => m.ForumUserId == userID));
            context.SaveChanges();
        }
        public void DeleteGroupMember(int userID, int groupID)
        {
            context.GroupMember
                .Remove(context.GroupMember
                .Where(m => m.ForumUserId == userID)
                .Where(m => m.GroupId == groupID)
                .SingleOrDefault());
            context.SaveChanges();
        }
    }
}
