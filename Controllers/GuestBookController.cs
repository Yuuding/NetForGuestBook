using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GuestBookSystem.Controllers
{
    public class GuestBookController : Controller
    {
        // GET: GuestBook
       
            GBSDBContext db = new GBSDBContext();
        public ActionResult Index()
        {
            return View(db.Guestbooks.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guestbook gb)
        {
            if (ModelState.IsValid)
            {
                //gb.CreatedOn = System.DateTime.Now;    //由数据库自动更新，不用后台处理
                db.Guestbooks.Add(gb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();  //错误的时候重新输入，保留上一次的数据
        }


    }
}