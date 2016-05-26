using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using FosterUniteForum.Domain.Utility;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.FosterUniteForum.Controllers
{
    public class TopicController : Controller
    {
        private ForumService fs = new ForumService();
        private TopicService ts = new TopicService();
        private PostService ps = new PostService();
        private ForumUserManager fum = new ForumUserManager();
        private AccessManagement am = new AccessManagement();
        private GroupMemberService gms = new GroupMemberService();
        private AccessMaskService ams = new AccessMaskService();
        private ForumAccessService fas = new ForumAccessService();

        private ForumUser user = new ForumUser();
        private Topic topic = new Topic();
        private Post post = new Post();
        private Forum forum = new Forum();
        private List<Post> postList = new List<Post>();


        [HttpGet]
        public ActionResult ViewTopic(int id)
        {
            topic = ts.GetTopicByTopicID(id);
            user = fum.GetUserByEmail(User.Identity.Name.ToString());
            if (user != null)
            {
                int userAccessFlag = GetUserAccessFlageByUserandForumID(user, topic.ForumId);
                ViewBag.isReplyable = am.isReplyable(userAccessFlag);
            }
            else
            {
                ViewBag.isReplyable = false;
            }
            ViewBag.Forum = fs.GetForumByID(topic.ForumId);
            ts.UpdateViewCount(id);
            return View(topic);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            user = fum.GetUserByEmail(User.Identity.Name.ToString());
            if (user != null &&
                am.isPostable(GetUserAccessFlageByUserandForumID(user, id)))
            {
                ViewBag.Forum = fs.GetForumByID(id);
                //Add drop down list
                Dictionary<int, string> topicTypeList = new TopicTypeCollection().GetTopicTypeDicList();
                List<SelectListItem> dropDownList = new List<SelectListItem>();
                foreach (KeyValuePair<int, string> type in topicTypeList)
                {
                    dropDownList.Add(new SelectListItem { Text = type.Value, Value = type.Key.ToString() });
                }
                ViewBag.forumID = id;
                ViewBag.TopicTypeList = dropDownList;
                return View();

            }
            else
            {
                return RedirectToAction("ViewForum", "Forum", new { id = id });
            }

        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(Topic topic, string message, int forumID)
        {
            user = fum.GetCurrentForumUser(User.Identity.GetUserId());
            string ip = Request.UserHostAddress;
            this.topic = await ts.AddTopic(topic, forumID, user);
            if (this.topic != null)
            {
                this.post = await ps.AddPost(this.topic.Title, message, ip, this.topic.Id, user);
                if (this.post != null)
                {
                    await fs.UpdateForumCountByForumID(forumID, this.topic.Id, this.post.Id, user, ForumCountUtilities.TOPIC);
                    return RedirectToAction("ViewForum", "Forum", new { id = forumID });
                }
            }
            return RedirectToAction("Create", forumID);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            post = ps.GetPostByID(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            post.Edited = DateTime.Today.ToString();
            ps.UpdatePostwithEditMethod(post);
            return RedirectToAction("ViewTopic", new { id = post.TopicId });
        }

        public PartialViewResult RenderPost(int topicID, int page, int pageSize)
        {
            ViewBag.topicID = topicID;
            postList = ps.GetPostListByTopicID(topicID);
            PagedList<Post> pagedPost = new PagedList<Post>(postList, page, pageSize);
            user = fum.GetCurrentForumUser(User.Identity.GetUserId());
            if (user != null)
            {

                ViewBag.currentUserID = user.Id;
            }
            else
            {
                ViewBag.currentUserID = 0;
            }
            return PartialView("_Post", pagedPost);
        }

        private int GetUserAccessFlageByUserandForumID(ForumUser user, int forumID)
        {
            if (gms.GetUserGroup(user.Id).Count != 0)
            {
                int user_group_ID = gms.GetGroupMemberByUserID(user.Id).GroupId;
                int user_AccessMask_ID = fas.GetForumAccess(forumID, user_group_ID).AccessMaskId;
                int user_AccessFlag = ams.GetAccessMaskbyID(user_AccessMask_ID).AccessFlag;
                return user_AccessFlag;
            }
            return 0;
        }

    }

}