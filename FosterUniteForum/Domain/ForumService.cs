using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain.Utility;

namespace FosterUniteForum.Domain
{
    public class ForumService : BasicService
    {
        private Forum forum;
        private List<Forum> forumList;
        private List<int> recuresiveDeleteForumIDList;
        public ForumService()
        {
            forum = new Forum();
            forumList = new List<Forum>();
            recuresiveDeleteForumIDList = new List<int>();
        }

        public ForumService(ForumDbContext context) : base(context) { }

        public bool AddForum(int categoryID, string forumName, int sortOrder, string forumDescription, int? parentForumID)
        {
            Forum tempForum = this.GetForum(forumName, categoryID, parentForumID);

            if (tempForum == null)
            {
                forum.CategoryId = categoryID;
                forum.Name = forumName;
                forum.SortOrder = sortOrder;
                forum.Description = forumDescription;
                forum.ParentForumId = parentForumID;
                forum.TopicCount = 0;
                forum.PostCount = 0;
                context.Forum.Add(forum);
                context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public bool AddSubForum(int categoryID, string forumName, int sortOrder, string forumDescription, int parentForumID)
        {
            Forum tempForum = this.GetSubForum(forumName, categoryID, parentForumID);

            if (tempForum == null)
            {
                forum.CategoryId = categoryID;
                forum.Name = forumName;
                forum.SortOrder = sortOrder;
                forum.Description = forumDescription;
                forum.ParentForumId = parentForumID;
                forum.TopicCount = 0;
                forum.PostCount = 0;
                context.Forum.Add(forum);
                context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public Forum GetForum(string forumName, int categoryID, int? parentForumID)
        {
            return context.Forum
                .Where(m => m.Name.Equals(forumName))
                .Where(m => m.CategoryId == categoryID)
                .Where(m => m.ParentForumId == parentForumID)
                .SingleOrDefault();
        }


        public Forum GetForumByID(int forumID)
        {
            return context.Forum
                .Where(m => m.Id == forumID)
                .SingleOrDefault();
        }

        public Forum GetSubForum(string forumName, int categoryID, int? parentForumID)
        {
            return context.Forum
                .Where(m => m.Name.Equals(forumName))
                .Where(m => m.CategoryId == categoryID)
                .Where(m => m.ParentForumId == parentForumID)
                .SingleOrDefault();
        }

        public List<Forum> GetForumList(int categoryID)
        {
            return context.Forum
                .Where(m => m.CategoryId == categoryID)
                .Where(m => m.ParentForumId == null)
                .ToList();
        }

        public List<Forum> GetSubForumList(Forum forum)
        {
            return context.Forum
                .Where(m => m.ParentForumId == forum.Id)
                .ToList();
        }

        public List<Forum> GetSubForumListByID(int forumID)
        {
            return context.Forum
                .Where(m => m.ParentForumId == forumID)
                .ToList();
        }

        public int GetForumID(string forumName, int categoryID, int? parentForumID)
        {
            return context.Forum
                .Where(m => m.Name.Equals(forumName))
                .Where(m => m.CategoryId == categoryID)
                .Where(m => m.ParentForumId == parentForumID)
                .SingleOrDefault()
                .Id;
        }

        public int GetSubForumID(string forumName, int categoryID, int? parentForumID)
        {
            return context.Forum
                .Where(m => m.Name.Equals(forumName))
                .Where(m => m.CategoryId == categoryID)
                .Where(m => m.ParentForumId == parentForumID)
                .SingleOrDefault()
                .Id;
        }

        public void UpdateForum(Forum forum)
        {
            this.forum = context.Forum.Where(m => m.Id == forum.Id).SingleOrDefault();
            this.forum.Name = forum.Name;
            this.forum.Description = forum.Description;
            this.forum.SortOrder = forum.SortOrder;
            context.SaveChanges();
        }

        public async Task<Forum> UpdateForumCountByForumID(int forumID, int topicID, int postID, ForumUser user, ForumCountUtilities count)
        {
            forum = GetForumByID(forumID);
            if (count == ForumCountUtilities.POST)
            {
                forum.PostCount++;
            }
            if (count == ForumCountUtilities.TOPIC)
            {
                forum.TopicCount++;
            }
            forum.LastPosted = DateTime.Now.ToString();
            forum.LastTopicId = topicID;
            forum.LastPostId = postID;
            forum.LastPostUserId = user.Id;
            forum.LastPostUsername = user.FirstName;
            await context.SaveChangesAsync();
            return GetForumByID(forumID);
        }

        public List<int> DeleteForum(string forumName, int categoryID, int? parentForumID)
        {
            int forumID = this.GetForumID(forumName, categoryID, parentForumID);
            //check if there is sub forum exist
            List<Forum> subForumList = this.GetSubForumListByParentForumID(forumID);

            if (subForumList.Count != 0)
            {
                foreach (Forum subForum in subForumList)
                {
                    if (!this.CheckIfHaveSubForum(subForum.Id))
                    {
                        recuresiveDeleteForumIDList.Add(subForum.Id);
                    }
                    else
                    {
                        recuresiveDeleteForumIDList.Add(subForum.Id);
                        this.DeleteForum(subForum.Name, subForum.CategoryId, subForum.ParentForumId);
                    }
                }
            }
            recuresiveDeleteForumIDList.Add(forumID);
            return recuresiveDeleteForumIDList;
        }
        public void DeleteForumAccessList()
        {
            ForumAccessService fas = new ForumAccessService();
            foreach (int subForumID in recuresiveDeleteForumIDList)
            {
                //Delete Forum Access Mask
                List<ForumAccess> forumAccessList = fas.GetForumAccessList(subForumID);
                if (forumAccessList.Count != 0)
                {
                    fas.DeleteForumAccess(subForumID);
                }
            }
        }
        public void DeleteForumRecuresively(List<int> deleteList)
        {
            for (int i = 0; i < deleteList.Count; i++)
            {
                int forumID = deleteList[i];
                if (!this.CheckIfHaveSubForum(deleteList[i])) // If current forum don't have a sub-forum
                {
                    if (this.GetForumByID(deleteList[i]) != null) // If there is forum with current forum id 
                    {
                        context.Forum.Remove(context.Forum.Where(m => m.Id == forumID).SingleOrDefault()); // Remove the corresponding record from database
                        context.SaveChanges();
                    }
                }
                else // if current forum id has a sub-forum
                {
                    deleteList.Insert(deleteList.Count, deleteList[i]); //Put current position of forum id at end of the delete list 
                    deleteList.RemoveAt(i); //delete element at current position 
                    this.DeleteForumRecuresively(deleteList);
                }
            }
        }


        public List<Forum> GetSubForumListByParentForumID(int? parentForumID)
        {
            return context.Forum.Where(m => m.ParentForumId == parentForumID).ToList();
        }

        public bool CheckIfHaveSubForum(int? parentForumID)
        {
            if (this.GetSubForumListByParentForumID(parentForumID).Count > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
