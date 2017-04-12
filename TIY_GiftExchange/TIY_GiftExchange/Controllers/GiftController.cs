using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIY_GiftExchange.Models;
using TIY_GiftExchange.Services;

namespace TIY_GiftExchange.Controllers
{
    public class GiftController : Controller
    {
        // GET: Gift
        public ActionResult Index()
        {
            var gifts = new GiftServices().GetAllGifts();
            return View(gifts);
        }

        //GET: Create Gift
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //Put into DB
            var gift = new Gift
            {
                Contents = collection["Contents"],
                GiftHint = collection["GiftHint"],
                ColorWrappingPaper = collection["ColorWrappingPaper"],
                Height = double.Parse(collection["Height"]),
                Depth = double.Parse(collection["Depth"]),
                Width = double.Parse(collection["Width"]),
                Weight = double.Parse(collection["Weight"]),
                IsOpened= bool.Parse(collection["IsOpened"])
            };
            var newGift= new GiftServices().CreateGift(gift);
            return RedirectToAction("Index");
        }

        //GET: Id to Edit
        public ActionResult Edit()
        {

            //get gift Id
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var gift = GiftServices.GetAllGifts();
            //Edit gift with respective Id
            UpdateModel(gift);
            var newGift = new GiftServices().EditGift();
            return RedirectToAction("Index");
        }
    }
}