using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain.Utility
{
    public enum GROUPNAME
    {
        Administrator = 1,
        Guest = 2,
        Member = 3,
        Moderator = 4
    }
    public class UserGroupNameCollection
    {
        public List<string> GetGroupNameList()
        {
            return Enum.GetNames(typeof(GROUPNAME)).ToList();
        }
        public Dictionary<int, string> GetGroupNameDicList()
        {
            Dictionary<int, string> groupList = new Dictionary<int, string>();
            int key = 1;
            foreach (string groupName in GetGroupNameList())
            {
                groupList.Add(key, groupName);
                key++;
            }
            return groupList;
        }
    }
}
