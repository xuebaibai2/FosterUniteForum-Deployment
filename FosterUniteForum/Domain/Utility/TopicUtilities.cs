using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain.Utility
{
    enum TOPICTYPE
    {
        Regular = 1,
        Announcement = 2,
        Sticky = 3
    }
    public class TopicTypeCollection
    {
        public List<string> GetTopicTypeList()
        {
            return Enum.GetNames(typeof(TOPICTYPE)).ToList();
        }

        public Dictionary<int, string> GetTopicTypeDicList()
        {
            Dictionary<int, string> topicTypeList = new Dictionary<int, string>();
            int key = 1;
            {
                foreach (string type in GetTopicTypeList())
                {
                    topicTypeList.Add(key, type);
                    key++;
                }
                return topicTypeList;
            }
        }
    }
}
