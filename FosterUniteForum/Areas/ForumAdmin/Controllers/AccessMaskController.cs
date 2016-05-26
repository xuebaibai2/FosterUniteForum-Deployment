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
    public class AccessMaskController : Controller
    {
        [HttpGet]
        public ActionResult Update(string maskName, int? boardIDnull)
        {
            int boardID = boardIDnull ?? 0;
            AccessMaskService ams = new AccessMaskService();
            AccessMaskCollection amc = new AccessMaskCollection();
            if (maskName == null || boardIDnull == null || ams.GetAccessMask(maskName, boardID) == null)
            {
                return Redirect("~/ForumAdmin");
            }
            AccessMask am = ams.GetAccessMask(maskName, boardID);

            ViewBag.activeMaskList = ams.GetAccessMaskFlag(am);
            ViewBag.accessFlag = am.AccessFlag;
            ViewBag.accessMaskID = ams.GetAccessMaskID(maskName, boardID);
            ViewBag.boardID = boardID;
            ViewBag.maskName = maskName;

            return View();
        }

        [HttpPost]
        public ActionResult Update(int accessMaskID, string Name, int accessFlag, int boardID, string preMaskName)
        {
            AccessMaskService ams = new AccessMaskService();
            if (!Name.Equals(preMaskName))
            {
                if (ams.GetAccessMask(Name, boardID) != null)
                {
                    TempData["Fail"] = "Mask name Already exist!";
                    string preURL = string.Format("~/ForumAdmin/AccessMask/Update?maskName={0}&boardIDnull={1}", preMaskName, boardID);
                    return Redirect(preURL);
                }
            }
            if (ams.UpdateAccessMask(accessMaskID, Name, accessFlag))
            {
                TempData["Success"] = "Updated Successfully!";
            }
            string url = string.Format("~/ForumAdmin/AccessMask/Update?maskName={0}&boardIDnull={1}", Name, boardID);
            return Redirect(url);
        }

        public ActionResult RemoveAccessMask(string maskName, int boardID)
        {
            AccessMaskService ams = new AccessMaskService();
            ams.DeleteAccessMask(maskName, boardID);
            string url = string.Format("~/ForumAdmin/Board/Update/{0}", boardID);
            return Redirect(url);
        }
    }
}