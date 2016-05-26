using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using FosterUniteForum.Domain.Utility;
using FosterUniteForum.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.ForumAdmin.Controllers
{
    [AdministratorAuthorize]
    public class CategoryController : Controller
    {
        [HttpGet]
        public ActionResult Update(string categoryName, int? boardIDnull)
        {
            int boardID = boardIDnull ?? 0;
            CategoryService cs = new CategoryService();
            ForumService fs = new ForumService();
            if (categoryName == null || boardIDnull == null || cs.GetCategory(categoryName, boardID) == null)
            {
                return Redirect("~/ForumAdmin");
            }
            Category category = cs.GetCategory(categoryName, boardID);
            ViewBag.forumList = fs.GetForumList(category.Id);
            ViewBag.preCategoryName = category.Name;
            ViewBag.boardID = category.BoardId;
            return View(category);
        }

        [HttpPost]
        public ActionResult Update(Category category,string SortOrder, string submitFlag, 
            string forumName, string forumSortOrder, string forumDescription,int boardIDnull, 
            string categoryName, string preCategoryName, int? parentForumID)
        {
            CategoryService cs = new CategoryService();
            if (submitFlag.Equals(SubmitFlagUtilities.UpdateCategory.ToString()))
            {
                //If Update Category
                if (!category.Name.Equals(preCategoryName))
                {
                    if (cs.GetCategory(category.Name, category.BoardId) != null)
                    {
                        TempData["Fail"] = "Category Name Already exist!";
                        return RedirectToAction("Update", new { categoryName = preCategoryName, boardIDnull = category.BoardId });
                    }
                }
                    cs.UpdateCategory(category);
                    TempData["Success"] = "Updated Successfully!";
                    return RedirectToAction("Update", new { categoryName = category.Name, boardIDnull = category.BoardId });
                
            }
            if (submitFlag.Equals(SubmitFlagUtilities.CreateForum.ToString()))
            {
                //If Create New Forum
                ForumService fs = new ForumService();
                if (fs.AddForum(category.Id, forumName, int.Parse(forumSortOrder), forumDescription, parentForumID))
                {
                    TempData["CreateForum"] = "Forum Created!";
                    return RedirectToAction("Update", new { categoryName = categoryName, boardIDnull = boardIDnull });
                }
                else
                {
                    TempData["ForumExist"] = "Forum name already exist!";
                    return RedirectToAction("Update", new { categoryName = categoryName, boardIDnull = boardIDnull });
                }
            }
            return View();
        }

        public ActionResult RemoveCategory(string categoryName, int boardID)
        {
            CategoryService cs = new CategoryService();
            cs.DeleteCategory(categoryName, boardID);
            string url = string.Format("~/ForumAdmin/Board/Update/{0}", boardID);
            return Redirect(url);
        }


    }
}