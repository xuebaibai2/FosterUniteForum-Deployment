using FosterUniteForum.Authorize;
using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using FosterUniteForum.Domain.Utility;
using FosterUniteForum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.ForumAdmin.Controllers
{
    [AdministratorAuthorize]
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            ForumUserManager fum = new ForumUserManager();
            ViewBag.UserList = fum.GetAllUserList();
            ViewBag.isPartial = false;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(RegisterViewModel model)
        {
            ForumUserManager fum = new ForumUserManager();

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                State = model.State,
                Birthday = model.Birthday,
                CompanyName = model.CompanyName,
                CompanyAddress = model.CompanyAddress,
                Gender = model.Gender,
                CreationDate = DateTime.Today.ToString(),
                Approved = false
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                using (var context = new ForumDbContext())
                {
                    var forumUser = new ForumUser
                    {
                        ProviderId = user.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailAddress = model.Email,
                        Gender = model.Gender,
                        City = model.City,
                        State = model.State,
                        Birthday = model.Birthday,
                        CompanyName = model.CompanyName,
                        CompanyAddress = model.CompanyAddress,
                        Deleted = false,
                        Active = false,
                        UserFlag = 0,
                        UseFullName = false,
                        ExternalAccount = false
                    };
                    context.ForumUser.Add(forumUser);
                    context.SaveChanges();
                }
                new RegistService().SetMemberGroup(user.Id, (int)GROUPNAME.Guest);
                TempData["SuccessAddUser"] = "User Created";
            }
            else
            {
                TempData["FailToAddUser"] = "Creation Failed";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            ForumUserManager fum = new ForumUserManager();
            GroupMemberService gms = new GroupMemberService();
            ForumUser user = fum.GetForumUser(id);
            //Add drop down list
            Dictionary<int, string> userGroupList = new UserGroupNameCollection().GetGroupNameDicList();
            List<SelectListItem> dropDownList = new List<SelectListItem>();
            dropDownList.Add(new SelectListItem { Text = "-- Select One--", Value = "0" });
            foreach (KeyValuePair<int, string> group in userGroupList)
            {
                dropDownList.Add(new SelectListItem { Text = group.Value, Value = group.Key.ToString() });
            }

            ViewBag.UserGroupList = gms.GetUserGroup(id);
            ViewBag.groupList = dropDownList;
            return View(user);
        }

        [HttpPost]
        public  ActionResult Update(ForumUser model, string submitFlag, int userGroup)
        {
            if (submitFlag.Equals(SubmitFlagUtilities.UpdateUser.ToString()))
            {
                //If Update User
                if (this.UpdateUser(model))
                    TempData["SuccessUpdateUser"] = "User Updated!";
                else
                    TempData["FailToUpdateUser"] = "Update Failed!";

                return RedirectToAction("Update", model.Id);
            }
            if (submitFlag.Equals(SubmitFlagUtilities.UpdateUserRole.ToString()))
            {
                //If Update User Role

            }
            if (submitFlag.Equals(SubmitFlagUtilities.AddUserGroup.ToString()))
            {
                //If Update User Group
                if (userGroup == 0)
                {
                    TempData["FailToAddGroup"] = "Please select proper value!";
                }
                else
                {
                    if (AddUserGroup(model, userGroup))
                        TempData["SuccessAddGroup"] = "Add Success!";
                    else
                        TempData["FailToAddGroup"] = "Add Failed!";
                }
                return RedirectToAction("Update", model.Id);
            }
            return View();
        }

        public ActionResult DeleteUser(int id)
        {
            ForumUserManager fum = new ForumUserManager();
            //ForumUser user = fum.GetForumUser(id);
            //ApplicationUser au = UserManager.FindById(user.ProviderId);
            fum.DeleteUser(id);
            //UserManager.Delete(au);
            return RedirectToAction("Index");
        }

        public PartialViewResult SearchResult(string search)
        {
            ForumUserManager fum = new ForumUserManager();
            List<ForumUser> matchedUserList = fum.SearchUserName(search);
            ViewBag.isPartial = true;
            return PartialView("_SearchResult", matchedUserList);
        }

        public bool UpdateUser(ForumUser model)
        {
            ForumUserManager fum = new ForumUserManager();

            ApplicationUser user = this.UserManager.FindById(model.ProviderId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = UserManager.Update(user);
            if (result.Succeeded)
            {

                fum.UpdateUser(model);
                return true;
            }
            else
                return false;
        }
        public ActionResult DeleteUserGroup(int userID, int groupID)
        {
            GroupMemberService gms = new GroupMemberService();
            gms.DeleteGroupMember(userID, groupID);
            return RedirectToAction("Update", new { id = userID });
        }
        public bool AddUserGroup(ForumUser model, int groupID)
        {
            GroupMemberService gms = new GroupMemberService();
            return gms.AddGroupMember(groupID, model.Id);
        }
    }
}