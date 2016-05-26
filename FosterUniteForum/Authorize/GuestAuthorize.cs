using FosterUniteForum.Domain;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FosterUniteForum.Domain.Utility;
using System.Collections.Generic;

namespace FosterUniteForum.Authorize
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class GuestAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            //string group = new ForumUserManager().GetUserGroup(HttpContext.Current.User.Identity.GetUserId());
            List<string> groupList = new ForumUserManager().GetUserGroupList(HttpContext.Current.User.Identity.GetUserId());
            if (groupList.Contains(GROUPNAME.Guest.ToString()) || groupList.Contains(GROUPNAME.Member.ToString()) || groupList.Contains(GROUPNAME.Administrator.ToString()))
            {
                return true;
            }
            return false;
        }
    }
}