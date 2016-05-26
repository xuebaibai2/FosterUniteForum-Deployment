using FosterUniteForum.Authorize;
using FosterUniteForum.Areas.ForumAdmin.Models;
using FosterUniteForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Areas.ForumAdmin.Controllers
{
    [AdministratorAuthorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(BoardService board)
        {
            if (board.GetBoardList().Count != 0)
            {
                ViewBag.BoardListCount = board.GetBoardListCount();
                ViewBag.BoardNameList = board.GetBoardNameeList();
                ViewBag.BoardDescriptionList = board.GetBoardDescriptionList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(BoardService boardService, BoardModel model)
        {
            if (ModelState.IsValid)
            {
                boardService = new BoardService();
                boardService.AddBoard(model.BoardName, model.BoardDescription);
                string currentURL = this.Request.UrlReferrer.AbsolutePath;
                return Redirect(currentURL);
            }
            return View(model);
        }
        public ActionResult RemoveBoard(string boardName)
        {
            BoardService bs = new BoardService();
            bs.DeleteBoard(boardName);
            return Redirect("~/ForumAdmin");
        }

    }
}