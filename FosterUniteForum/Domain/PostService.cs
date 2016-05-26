using FosterUniteForum.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class PostService : BasicService
    {
        private Post post;
        private List<Post> postList;
        private TopicService ts = new TopicService();

        public PostService()
        {
            post = new Post();
            postList = new List<Post>();
        }

        public PostService(ForumDbContext context) : base(context) { }

        public async Task<Post> AddPost(string subject, string body, string ip, int topicID, ForumUser user)
        {
            if (GetTopicCount(topicID) == 0)
            {
                this.post.Position = 0;
            }
            else
            {
                this.post.Position = GetTopicCount(topicID);
            }
            
            this.post.Indent = 0;
            this.post.Subject = subject;
            this.post.Body = body;
            this.post.IP = ip;
            this.post.Flag = 0;
            this.post.TopicId = topicID;
            this.post.AuthorId = user.Id;
            this.post.AuthorName = user.FirstName;
            this.post.Posted = DateTime.Now.ToString();
            this.post.SpamScore = 0;
            this.post.SpamReporters = 0;
            context.Post.Add(post);
            await context.SaveChangesAsync();
            ts.UpdatePostCount(topicID);
            return GetFirstPostByTopicID(topicID);
        }

        public Post UpdatePostwithEditMethod(Post post)
        {
            this.post = GetPostByID(post.Id);
            this.post.Subject = post.Subject;
            this.post.Body = post.Body;
            this.post.EditReason = post.EditReason;
            this.post.Edited = post.Edited;
            context.SaveChanges();
            return this.post;
        }

        public int GetTopicCount(int topicID)
        {
            return context.Post
                .Where(m => m.TopicId == topicID)
                .Count();
        }
        public Post GetFirstPostByTopicID(int topicID)
        {
            return context.Post
                      .Where(m => m.TopicId == topicID)
                      .Where(m => m.Position == 0)
                      .SingleOrDefault();
        }

        public List<Post> GetPostListByTopicID(int id)
        {
            return context.Post
                .Where(m => m.TopicId == id)
                .ToList();
        }

        public Post GetPostByID(int postID)
        {
            return context.Post.Where(m => m.Id == postID).SingleOrDefault();
        }

        public Post GetPostByTopicIDandPosition(int topicID, int position)
        {
            return context.Post
                .Where(m => m.TopicId == topicID)
                .Where(m => m.Position == position)
                .SingleOrDefault();
        }
    }
}
