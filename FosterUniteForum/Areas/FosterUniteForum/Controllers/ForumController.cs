using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using FosterUniteForum.Domain.Utility;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.FosterUniteForum.Controllers
{
    public class ForumController : Controller
    {
        private ForumService fs = new ForumService();
        private CategoryService cs = new CategoryService();
        private TopicService ts = new TopicService();
        private GroupMemberService gms = new GroupMemberService();
        private AccessMaskService ams = new AccessMaskService();
        private ForumAccessService fas = new ForumAccessService();
        private ForumUserManager fum = new ForumUserManager();
        private AccessManagement am = new AccessManagement();

        private ForumUser user = new ForumUser();
        private Topic topic = new Topic();
        private List<Topic> topicList = new List<Topic>();
        private Category category = new Category();
        private Forum forum = new Forum();

        [HttpGet]
        public ActionResult ViewForum(int id)
        {
            user = fum.GetUserByEmail(User.Identity.Name.ToString());
            if (user != null)
            {
                int userAccessFlag = GetUserAccessFlageByUserandForumID(user, id);
                ViewBag.isPostable = am.isPostable(userAccessFlag);
            }
            else
            {
                ViewBag.isPostable = false;
            }

            forum = fs.GetForumByID(id);
            ViewBag.categoryName = cs.GetCategoryByID(forum.CategoryId).Name;

            return View(forum);
        }

        [HttpGet]
        public ActionResult ViewProfile(int id)
        {
            user = fum.GetForumUser(id);
            return View(user);
        }

        public PartialViewResult RenderTopic(int forumID, int page, int pageSize)
        {
            ViewBag.forumID = forumID;
            topicList = ts.GetTopicListByForumID(forumID);
            PagedList<Topic> pagedTopic = new PagedList<Topic>(topicList, page, pageSize);
            return PartialView("_Forum", pagedTopic);
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