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
            return View(gifts); //return a List of the Gift Pool
        }

        //GET: Create Gift
        public ActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //Create new Gift
            var gift = new Gift();
            UpdateModel(gift);
            //Put into DB
            var newGift= new GiftServices().CreateGift(gift);
            return RedirectToAction("Index");
        }

        //GET: Id to Edit
        public ActionResult Edit(int id)
        {
            //get gift Id
            var gift = GiftServices.GetGift(id);
            return View(gift);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var gift = new Gift();
            //Edit gift with respective Id
            UpdateModel(gift);
            var newGift = new GiftServices().EditGift(gift);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var gift = GiftServices.GetGift(id);
            return View(gift);
        }
        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var gift = new Gift();
            gift.Id= id;
            var newGift= new GiftServices().DeleteGift(gift);
            return RedirectToAction("Index");
        }

        public ActionResult Unopened()
        {
            var gift = new GiftServices().UnopenedGifts();
            return View(gift);
        }

        public ActionResult Open(int id)
        {
            var gift = GiftServices.GetGift(id);
            return View(gift);
        }

        [HttpPost]
        public ActionResult Open(int id, FormCollection collection)
        {
            var gift = new Gift();
            gift.Id = id;
            var newGift = new GiftServices().OpenGift(gift);
            return RedirectToAction("Index");
        }
    }
}