using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBookSystem.Controllers
{
    //管理员控制器
    [Authorize]
    public class AccountController : Controller
    {

        GBSDBContext db = new GBSDBContext();
        // GET: Account
        public ActionResult Index(String email)
        {
            Session["UserEmail"] = email;
            return View(db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList());
        }
        
       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UserLogin", "Home");
        }
        public ActionResult Delete(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var gb = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(gb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CheckMessage(int id)
        {
            var gb = db.Guestbooks.Find(id);

            return View(gb);
        }
        [HttpPost, ActionName("CheckMessage")]
        public ActionResult CheckMessage1(int id)
        {

            var gb = db.Guestbooks.Find(id);
            gb.isPass = true;
            db.SaveChanges();
            return RedirectToAction("Index", new { target = "fc" });


        }
    }
}