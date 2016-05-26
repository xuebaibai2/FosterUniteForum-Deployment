using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain;
using FosterUniteForum.Domain.Utility;
using FosterUniteForum.Areas.ForumAdmin.Models;
using FosterUniteForum.Authorize;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.ForumAdmin.Controllers
{
    [AdministratorAuthorize]
    public class BoardController : Controller
    {
        [HttpGet]
        public ActionResult Update(string id)
        {
            BoardService bs = new BoardService();
            AccessMaskService ams = new AccessMaskService();
            CategoryService cs = new CategoryService();

            int boardID;
            if (int.TryParse(id, out boardID))
            {
                if (bs.GetBoardIDList().Contains(int.Parse(id)))
                {
                    ViewBag.boardName = bs.GetBoard(boardID).Name;
                    ViewBag.boardDescription = bs.GetBoard(boardID).Description;
                    ViewBag.accessMaskList = ams.GetMaskNameMaskFlagList(boardID);
                    ViewBag.categoryList = cs.GetCategoryList(boardID);
                    return View();
                }
            }
            return Redirect("~/ForumAdmin");
        }

        [HttpPost]
        public ActionResult Update(Board model, string boardID, string submitFlag, string accessFlag, 
            string maskName, string categoryName, string categorySortOrder)
        {

            if (submitFlag.Equals(SubmitFlagUtilities.UpdateBoard.ToString()))
            {
                //If update board 
                BoardService bs = new BoardService();
                bs.UpdateBoard(int.Parse(boardID), model.Name, model.Description);
                TempData["Success"] = "Updated Successfully!";
                return RedirectToAction("Update");
            }

            if (submitFlag.Equals(SubmitFlagUtilities.CreateAccessMask.ToString()))
            {
                //If Craete Access Mask
                AccessMaskService ams = new AccessMaskService();
                if (ams.AddAccessMask(int.Parse(boardID), maskName, int.Parse(accessFlag)))
                {
                    TempData["CreateAccessMask"] = "Mask Created!";
                    return RedirectToAction("update");
                }
                else
                {
                    TempData["AccessMaskExist"] = "Mask name already exist!";
                    return RedirectToAction("update");
                }

            }

            if (submitFlag.Equals(SubmitFlagUtilities.CreateCategory.ToString()))
            {
                //If Craete Categroy
                CategoryService cs = new CategoryService();
                if (cs.AddCategory(int.Parse(boardID),categoryName,int.Parse(categorySortOrder)))
                {
                    TempData["CreateCategory"] = "Category Created!";
                    return RedirectToAction("update");
                }
                else
                {
                    TempData["CategoryExist"] = "Category name already exist!";
                    return RedirectToAction("update");
                }
            }
            return View();
        }

    }
}