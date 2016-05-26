using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PinPayments;
using System.Web.Configuration;
using PinPayments.Models;
using FosterUniteForum.Domain;
using FosterUniteForum.Data.EntityModel;

namespace FosterUniteForum.Controllers
{
    public class DonateController : Controller
    {
        private Card card;
        private PostCharge charge;
        private DonateReceiptService drs = new DonateReceiptService();
        private DonateReceipt receipt;
        private PinService ps = new PinService(WebConfigurationManager.AppSettings["Secret_API"]);

        [HttpGet]
        public ActionResult Index()
        {
            card = new Card();
            return View(card);
        }

        [HttpPost]
        public ActionResult Index(Card card)
        {
            CardCreateResponse cr = ps.CardCreate(card);
            if (cr.Error == null)
            {
                return RedirectToAction("Donate", new { ct = cr.Response.Token });
            }
            else
            {
                TempData["InvalidCard"] = cr.Messages[0].Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Donate(string ct)
        {
            if (ct != null)
            {
                ViewBag.cardToken = ct;
                return View();
            }
            //return View();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Donate(Charge charge, string cardToken)
        {
            this.charge = new PostCharge()
            {
                Amount = charge.Amount,
                CardToken = cardToken,
                Currency = "AUD",
                Description = charge.Description,
                Email = charge.Email,
                IPAddress = Request.ServerVariables["REMOTE_ADDR"]
            };
            ChargeResponse response = ps.Charge(this.charge);

            if (response.Error == null)
            {
                if (response.Charge.Success)
                {
                    receipt = new DonateReceipt()
                    {
                        Amount = response.Charge.Amount.ToString(),
                        Card_Token = response.Charge.Card_Token,
                        Created = response.Charge.Created,
                        Currency = response.Charge.Currency,
                        Description = response.Charge.Description,
                        Email = response.Charge.Email,
                        IP_address = response.Charge.IP_address,
                        Token = response.Charge.Token
                    };
                    drs.AddReceipt(receipt);
                    return RedirectToAction("Receipt", new { token = receipt.Token });
                }
                else
                {
                    TempData["PurchaceFailed"] = response.Messages[0].Message;
                    return RedirectToAction("Donate", new { ct = cardToken });
                }
            }
            else
            {
                TempData["PurchaceFailed"] = response.Messages[0].Message;
                return RedirectToAction("Donate", new { ct = cardToken });
            }
        }

        [HttpGet]
        public ActionResult Receipt(string token)
        {
            if (token != null)
            {
                this.receipt = drs.GetDonateReceiptByToken(token);
            }
            if (receipt != null)
            {

                return View(receipt);
            }
            else
            {
                return RedirectToAction("Index", "Donate");
            }
        }
    }
}