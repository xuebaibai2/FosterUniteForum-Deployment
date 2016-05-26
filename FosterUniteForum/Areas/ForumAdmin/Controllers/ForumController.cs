using FosterUniteForum.Authorize;
using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using FosterUniteForum.Domain.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.ForumAdmin.Controllers
{
    [AdministratorAuthorize]
    public class ForumController : Controller
    {
        [HttpGet]
        public ActionResult Update(int id)
        {
            ForumService fs = new ForumService();
            GroupService gs = new GroupService();
            ForumAccessService fas = new ForumAccessService();
            AccessMaskService ams = new AccessMaskService();
            Forum forum = fs.GetForumByID(id);
            ViewBag.forumAccessList = fas.GetForumAccessList(id);
            ViewBag.forumList = fs.GetSubForumList(forum);
            ViewBag.groupList = gs.GetGroupList();
            List<AccessMask> accessMaskList = ams.GetAccessMaskList(forum.Category.BoardId);
            List<SelectListItem> dropDownList = new List<SelectListItem>();
            dropDownList.Add(new SelectListItem { Text = "-- Select One--", Value = "0" });
            foreach (AccessMask accessMask in accessMaskList)
            {
                dropDownList.Add(new SelectListItem { Text = accessMask.Name, Value = accessMask.AccessFlag.ToString() });
            }
            ViewBag.accessMaskList = dropDownList;
            if (fas.GetForumAccessList(id).Count < gs.GetGroupList().Count)
            {
                ViewBag.groupList = gs.GetGroupList();
                ViewBag.boardID = forum.Category.BoardId;
                ViewBag.forumID = id;
                ViewBag.forumName = forum.Name;
                ViewBag.categoryID = forum.CategoryId;
                return PartialView("_InitialPermission", forum);
            }
            return View(forum);

        }

        [HttpPost]
        public ActionResult Update(Forum forum, string forumDescription,
            string preForumName, string submitFlag, int? accessMaskFlagnull,
            int? boardIDnull, int? groupIDnull, int? forumIDnull, int? categoryIDnull, int? parentForumID)
        {
            ForumService fs = new ForumService();
            ForumAccessService fas = new ForumAccessService();
            AccessMaskService ams = new AccessMaskService();
            if (submitFlag.Equals(SubmitFlagUtilities.UpdateForum.ToString()))
            {
                //If Update Forum
                if (!forum.Name.Equals(preForumName))
                {
                    if (fs.GetForum(forum.Name, forum.CategoryId, parentForumID) != null)
                    {
                        TempData["Fail"] = "Forum Name Already exist!";
                        return RedirectToAction("Update", new { id = forum.Id });
                    }
                }
                fs.UpdateForum(forum);
                TempData["Success"] = "Updated Successfully!";
                return RedirectToAction("Update", new { id = forum.Id });

            }
            else if (submitFlag.Equals(SubmitFlagUtilities.CreateSubForum.ToString()))
            {
                //If Create Sub Forum
                if (fs.AddSubForum(forum.CategoryId, forum.Name, forum.SortOrder, forumDescription, forum.Id))
                {
                    TempData["CreateSubForum"] = "Sub-Forum Created!";
                    return RedirectToAction("Update", new { id = forum.Id });
                }
                else
                {
                    TempData["SubForumExist"] = "Sub-Forum name already exist!";
                    return RedirectToAction("Update", new { id = forum.Id });
                }
            }
            else if (submitFlag.Equals(SubmitFlagUtilities.UpdateForumPermission.ToString()))
            {
                //If Update Forum User Group Access Permission
                int accessMaskFlag = accessMaskFlagnull ?? 0;
                int boardID = boardIDnull ?? 0;
                int groupID = groupIDnull ?? 0;
                int forumID = forumIDnull ?? 0;
                int categoryID = categoryIDnull ?? 0;
                int accessMaskID = ams.GetAccessMaskIDbyFlag(accessMaskFlag, boardID);
                if (accessMaskID == 0)
                {
                    TempData["FailToAddPermission"] = "Please Select proper value!";
                    return RedirectToAction("Update", new { id = forum.Id });
                }
                if (fas.AddForumAccess(forumID, groupID, accessMaskID))
                {
                    return RedirectToAction("Update", new { id = forum.Id });
                }
                else
                {
                    fas.UpdateForumAccess(forumID, groupID, accessMaskID);
                    return RedirectToAction("Update", new { id = forum.Id });
                }

            }
            return View();
        }

        public ActionResult RemoveForum(string forumName, int categoryID, int boardID,
            string categoryName, int? parentForumID)
        {
            ForumService fs = new ForumService();
            Forum forum = fs.GetForum(forumName, categoryID, parentForumID);
            List<int> deleteList = fs.DeleteForum(forumName, categoryID, parentForumID);
            fs.DeleteForumAccessList();
            fs.DeleteForumRecuresively(deleteList);
            if (parentForumID == null)
            {
                return RedirectToAction("Update", "Category", new { categoryName = categoryName, boardIDnull = boardID });
            }
            return RedirectToAction("Update", new { id = parentForumID });
        }

        public string GetAccessMaskName(int forumID, int groupID, int boardID)
        {
            ForumAccessService fas = new ForumAccessService();
            AccessMaskService ams = new AccessMaskService();
            if (fas.GetForumAccess(forumID, groupID) == null)
            {
                return "";
            }
            int accessMaskID = fas.GetForumAccess(forumID, groupID).AccessMaskId;
            return ams.GetAccessMaskbyID(accessMaskID).Name;
        }

        public ActionResult UpdateRedirect(string forumName, int? categoryIDnull, int? parentForumID)
        {
            int categoryID = categoryIDnull ?? 0;
            ForumService fs = new ForumService();

            //If wrong route value been passed redirect to forumadmin home page
            if (forumName == null || categoryIDnull == null || fs.GetForum(forumName, categoryID, parentForumID) == null)
            {
                return Redirect("~/ForumAdmin");
            }
            Forum forum = fs.GetForum(forumName, categoryID, parentForumID);
            return RedirectToAction("Update", new { id = fs.GetForumID(forumName, categoryID, parentForumID) });
        }
    }
}