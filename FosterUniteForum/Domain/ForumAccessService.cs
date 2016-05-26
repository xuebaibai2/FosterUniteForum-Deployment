using FosterUniteForum.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class ForumAccessService : BasicService
    {
        private ForumAccess forumAccess;
        private List<ForumAccess> forumAccessList;
        private GroupService groupService;
        private AccessMaskService accessMaskService;

        public ForumAccessService()
        {
            forumAccess = new ForumAccess();
            forumAccessList = new List<ForumAccess>();
            groupService = new GroupService();
            accessMaskService = new AccessMaskService();
        }

        public ForumAccessService(ForumDbContext context) : base(context) { }

        public bool AddForumAccess(int forumID, int groupID, int accessMaskID)
        {
            ForumAccess tempForumAccess = this.GetForumAccess(forumID, groupID);

            if (tempForumAccess == null)
            {

                forumAccess.ForumId = forumID;
                forumAccess.GroupId = groupID;
                forumAccess.AccessMaskId = accessMaskID;
                context.ForumAccess.Add(forumAccess);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void UpdateForumAccess(int forumID, int groupID, int accessMaskID)
        {
            this.forumAccess = this.GetForumAccess(forumID, groupID);
            this.forumAccess.ForumId = forumID;
            this.forumAccess.GroupId = groupID;
            this.forumAccess.AccessMaskId = accessMaskID;
            context.SaveChanges();
        }

        public ForumAccess GetForumAccess(int forumID, int groupID)
        {
            return context.ForumAccess
                .Where(m => m.ForumId == forumID)
                .Where(m => m.GroupId == groupID)
                .SingleOrDefault();
        }

        public List<ForumAccess> GetForumAccessList(int forumID)
        {
            return context.ForumAccess.Where(m => m.ForumId == forumID).ToList();
        }

        public void DeleteForumAccess(int forumID)
        {
            context.ForumAccess.RemoveRange(context.ForumAccess.Where(m => m.ForumId == forumID));
            context.SaveChanges();
        }


        public void DeleteForumAccessByGroupID(int groupID)
        {
            context.ForumAccess.RemoveRange(context.ForumAccess.Where(m => m.GroupId == groupID));
            context.SaveChanges();
        }
    }
}
