using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using FosterUniteForum.Domain.Utility;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.FosterUniteForum.Controllers
{
    public class PostController : Controller
    {
        private ForumService fs = new ForumService();
        private TopicService ts = new TopicService();
        private PostService ps = new PostService();
        private ForumUserManager fum = new ForumUserManager();
        private GroupMemberService gms = new GroupMemberService();
        private AccessMaskService ams = new AccessMaskService();
        private ForumAccessService fas = new ForumAccessService();
        private AccessManagement am = new AccessManagement();

        private ForumUser user = new ForumUser();
        private Topic topic = new Topic();
        private Post post = new Post();
        private Forum forum = new Forum();
        private List<Post> postList = new List<Post>();

        [HttpGet]
        public ActionResult Create(int id) //id is topic id not the post id
        {
            topic = ts.GetTopicByTopicID(id);

            user = fum.GetUserByEmail(User.Identity.Name.ToString());
            if (user != null &&
                am.isReplyable(GetUserAccessFlageByUserandForumID(user, topic.ForumId)))
            {
                post = ps.GetPostByTopicIDandPosition(id, 0);
                ViewBag.Forum = fs.GetForumByID(topic.ForumId);
                return View(post);
            }
            else
            {
                return RedirectToAction("ViewTopic", "Topic", new { id = id });
            }

        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(Post post, string message, int mainPostID)
        {
            Post mainPost = ps.GetPostByID(mainPostID);
            string ip = Request.UserHostAddress;
            user = fum.GetCurrentForumUser(User.Identity.GetUserId());
            this.post = await ps.AddPost(post.Subject, message, ip, mainPost.TopicId, user);
            if (this.post != null)
            {
                await fs.UpdateForumCountByForumID(mainPost.Topic.ForumId, mainPost.TopicId, post.Id, user, ForumCountUtilities.POST);
                ts.UpdateLastPostUsernameandDateByLastPost(this.post.TopicId, user.FirstName, DateTime.Now.ToString());
                return RedirectToAction("ViewTopic", "Topic", new { id = mainPost.TopicId });
            }
            return View();
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