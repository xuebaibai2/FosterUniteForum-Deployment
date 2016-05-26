using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.FosterUniteForum.Controllers
{
    public class HomeController : Controller
    {

        private BoardService bs = new BoardService();
        private CategoryService cs = new CategoryService();
        private ForumService fs = new ForumService();

        private Board board = new Board();
        private Category category = new Category();
        private Forum forum = new Forum();
        private List<Category> categoryList = new List<Category>();
        private List<Forum> forumList = new List<Forum>();

        [HttpGet]
        public ActionResult Index()
        {
            //Get First board only
            board = bs.GetBoard(1);

            //Get board Categroy list
            categoryList = cs.GetCategoryList(1);
            ViewBag.categoryList = categoryList;

            //Get Forum List
            //forumList = fs.GetForumList();
            return View(board);
        }
        
        public PartialViewResult RenderForum(int categoryID)
        {
            forumList = fs.GetForumList(categoryID);
            return PartialView("_Forum", forumList);
        }
    }
}