using FosterUniteForum.Data.EntityModel;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class ForumUserManager : BasicService
    {
        private ForumUser user;
        private List<ForumUser> userList;

        public ForumUserManager(ForumDbContext context) : base(context) { }

        public ForumUserManager()
        {
            user = new ForumUser();
            userList = new List<ForumUser>();
        }

        public void AddForumUser(ForumUser forumUser)
        {

        }

        public ForumUser GetCurrentForumUser(string userID)
        {
            return this.user = context
                   .ForumUser.Where(m => m.ProviderId.Equals(userID))
                   .SingleOrDefault() ?? null;
        }
        public List<string> GetUserGroupList(string userID)
        {
            List<string> groupList = new List<string>();
            if (GetCurrentForumUser(userID) == null)
                return null;
            var forumID = GetCurrentForumUser(userID).Id;

            List<int> userGroupID = context.GroupMember
                .Where(m => m.ForumUserId == forumID)
                .Select(m => m.GroupId).ToList();
            foreach (int groupID in userGroupID)
            {
                groupList.Add(context.Group.Where(m => m.Id == groupID)
                .Select(m => m.Name).SingleOrDefault());
            }
            return groupList;
        }

        public List<ForumUser> SearchUserName(string searchedName)
        {
            List<ForumUser> matchedNameList = new List<ForumUser>();
            userList = this.GetAllUserList();

            foreach (ForumUser forumUser in userList)
            {
                if(forumUser.FirstName.ToLower().Contains(searchedName.ToLower()))
                {
                    matchedNameList.Add(forumUser);
                }
            }
            return matchedNameList;
        }

        public void UpdateUser(ForumUser model)
        {
            this.user = this.GetForumUser(model.Id);
            this.user.FirstName = model.FirstName;
            this.user.LastName = model.LastName;
            this.user.Deleted = model.Deleted;
            this.user.Active = model.Active;
            context.SaveChanges();
        }

        public List<ForumUser> GetAllUserList()
        {
            return context.ForumUser.ToList();
        }

        public ForumUser GetForumUser(int userID)
        {
            return context.ForumUser.Where(m => m.Id == userID).SingleOrDefault();
        }

        public int GetUserID(string userID)
        {
            if (GetCurrentForumUser(userID) == null)
                return 0;
            return GetCurrentForumUser(userID).Id;
        }

        public void DeleteUser(int id)
        {
            user = GetUserByID(id);
            user.Deleted = true;
            context.SaveChanges();
        }

        public ForumUser GetUserByID(int userID)
        {
            return context.ForumUser.Where(m => m.Id == userID).Single();
        }

        public string GetUserFirstName(string userID)
        {
            if (GetCurrentForumUser(userID) == null)
                return null;
            return GetCurrentForumUser(userID).FirstName;
        }

        public ForumUser GetUserByEmail(string email)
        {
            return context.ForumUser.Where(m => m.EmailAddress.Equals(email)).SingleOrDefault();
        }

        public void UpdateLastLoginDate(string email)
        {

            ForumUser tempUser = GetUserByEmail(email);
            tempUser.LastVisit = DateTime.Now.ToString();
            context.SaveChanges();
        }

        public void UpdateLastLoginIP(string email, string ip)
        {
            ForumUser tempUser = GetUserByEmail(email);
            tempUser.LastIP = ip;
            context.SaveChanges();
        }
    }
}
