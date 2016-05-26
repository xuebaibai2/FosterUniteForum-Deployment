using FosterUniteForum.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class GroupService : BasicService
    {
        private Group group;
        private List<Group> groupList;
        private List<string> groupNameList;
        public GroupService()
        {
            group = new Group();
            groupList = new List<Group>();
            groupNameList = new List<string>();
        }
        public GroupService(ForumDbContext context) : base(context) { }

        public void AddGroup(string groupName)
        {
            group.Name = groupName;
            context.Group.Add(group);
            context.SaveChanges();
        }

        public List<Group> GetGroupList()
        {
            return this.groupList = context.Group.ToList();
        }

        public bool UpdateGroup(Group group)
        {
            this.group = this.GetGroup(group.Id);
            if (group.Name.Equals(this.group.Name))
            {
                return true;
            }
            else if (this.GetGroupNameList().Contains(group.Name))
            {
                return false;
            }
            else
            {
                this.group.Name = group.Name;
                context.SaveChanges();
                return true;
            }
        }

        public Group GetGroup(int id)
        {
            return context.Group.Where(m => m.Id == id).SingleOrDefault();
        }

        public List<int> GetGroupIDList()
        {
            List<Group> tempGroupList = this.GetGroupList();
            List<int> groupIDList = new List<int>();
            foreach (Group group in tempGroupList)
            {
                groupIDList.Add(group.Id);
            }
            return groupIDList;
        }

        public List<string> GetGroupNameList()
        {
            return this.groupNameList = context.Group.Select(m => m.Name).ToList();
        }

        public void DeleteGroup(int groupID)
        {
            context.Group.Remove(context.Group.Where(m => m.Id == groupID).SingleOrDefault());
            context.SaveChanges();
        }
    }
}
