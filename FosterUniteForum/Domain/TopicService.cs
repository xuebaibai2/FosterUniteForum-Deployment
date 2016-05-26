using FosterUniteForum.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class TopicService : BasicService
    {
        private Topic topic;
        private List<Topic> topicList;

        public TopicService()
        {
            topic = new Topic();
            topicList = new List<Topic>();
        }

        public TopicService(ForumDbContext context) : base(context) { }

        public async Task<Topic> AddTopic(Topic topic, int forumID, ForumUser user)
        {
            this.topic.Title = topic.Title;
            this.topic.ViewCount = 1;
            this.topic.PostCount = 1;
            this.topic.Flag = 0;
            this.topic.Type = topic.Type;
            this.topic.LastPosted = DateTime.Now.ToString();
            this.topic.ForumId = forumID;
            this.topic.AuthorId = user.Id;
            this.topic.AuthorName = user.FirstName;
            this.topic.LastPostAuthorId = user.Id;
            this.topic.LastPostUsername = user.FirstName;
            this.topic.SpamScore = 0;
            this.topic.SpamReporters = 0;
            this.topic.Posted = DateTime.Now.ToString();
            context.Topic.Add(this.topic);
            await context.SaveChangesAsync();
            return GetTopicByPostDate(this.topic.Posted);
        }
        public void UpdatePostCount(int topicID)
        {
            this.topic = GetTopicByTopicID(topicID);
            this.topic.PostCount++;
            context.SaveChanges();
        }
        public void UpdateViewCount(int topicID)
        {
            this.topic = GetTopicByTopicID(topicID);
            this.topic.ViewCount++;
            context.SaveChanges();
        }
        public Topic GetTopicByTopicID(int id)
        {
            return context.Topic.Where(m => m.Id == id).SingleOrDefault();
        }

        public Topic GetTopicByPostDate(string postDate)
        {
            return context.Topic.Where(m => m.Posted.Equals(postDate)).SingleOrDefault();
        }

        public List<Topic> GetTopicListByForumID(int forumID)
        {
            return context.Topic
                          .Where(m => m.ForumId == forumID)
                          .ToList();
        }

        public void UpdateLastPostUsernameandDateByLastPost(int topicID, string authorName, string posted)
        {
            this.topic = GetTopicByTopicID(topicID);
            this.topic.LastPostUsername = authorName;
            this.topic.LastPosted = posted;
            context.SaveChanges();
        }
    }
}
