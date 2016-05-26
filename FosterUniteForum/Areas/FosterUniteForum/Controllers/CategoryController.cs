using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.FosterUniteForum.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService cs = new CategoryService();
        private ForumService fs = new ForumService();

        private Category category = new Category();
        private Forum forum = new Forum();

        [HttpGet]
        public ActionResult ViewCategory(int id)
        {
            category = cs.GetCategoryByID(id);
            return View(category);
        }
    }
}