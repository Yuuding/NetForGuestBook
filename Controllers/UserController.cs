using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBookSystem.Controllers
{
    
    public class UserController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        
        public ActionResult Index()
        {
            //Session["UserId"]= id;
            //Session["AuthorEmail"] = email;
            var gb = db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList();
        
            return View(gb);
        }
        // GET: User
        public ActionResult MyWords()
        {
           
            var gb = db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList();
            return View(gb);
        }
        public ActionResult CreatMessage()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult CreatMessage(Guestbook gb)
        {
            if (ModelState.IsValid)
            {
                //gb.CreatedOn = System.DateTime.Now;    //由数据库自动更新，不用后台处理
                gb.UserId = (int)Session["UserId"];
                
                db.Guestbooks.Add(gb);
                db.SaveChanges();
                return RedirectToAction("MyWords");
            }
            return View();  //错误的时候重新输入，保留上一次的数据
           
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UserLogin", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}