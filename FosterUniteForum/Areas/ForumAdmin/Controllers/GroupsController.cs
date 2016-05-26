using FosterUniteForum.Domain;
using FosterUniteForum.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FosterUniteForum.Data.EntityModel;

namespace FosterUniteForum.Areas.ForumAdmin.Controllers
{
    [AdministratorAuthorize]
    public class GroupsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            GroupService groupService = new GroupService();
            ViewBag.GroupList = groupService.GetGroupList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Group model)
        {
            GroupService groupService = new GroupService();
            if (ModelState.IsValid)
            {
                groupService.AddGroup(model.Name);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            GroupService gs = new GroupService();
            Group group = gs.GetGroup(id);
            return View(group);
        }
        [HttpPost]
        public ActionResult Update(Group group)
        {
            GroupService gs = new GroupService();
            if (ModelState.IsValid)
            {
                if (gs.UpdateGroup(group))
                {
                    TempData["GroupUpdateSuccess"] = "Group Name Updated!";

                }
                else
                {
                    TempData["GroupUpdateFail"] = "Group Name Already Exist!";
                }
            }
            return View(group);
        }

        public ActionResult RemoveGroup(int id)
        {
            ForumAccessService fas = new ForumAccessService();
            GroupService gs = new GroupService();

            fas.DeleteForumAccessByGroupID(id);
            gs.DeleteGroup(id);
            return RedirectToAction("Index");
        }
    }
}