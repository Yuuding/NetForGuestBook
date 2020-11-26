using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBookSystem.Controllers
{
    public class HomeController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        //UserLogin
        public ActionResult UserLogin(User user)
        {
            var dbUser = db.Users.Where(a => a.Name == user.Name && a.Password == user.Password).FirstOrDefault();
            
               
            if (ModelState.IsValid)
            {
                
                if (dbUser != default)
                {
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                      1, dbUser.UserId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, dbUser.Srole.ToString());

                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                        authCookie.HttpOnly = true;

                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                        FormsAuthentication.SetAuthCookie(user.Name, false);
                    //判断是不是管理员

                    if (dbUser.Srole.ToString() == "管理员")
                        return RedirectToAction("Index", "Account", new { email = dbUser.Email.ToString() });
                    else if (dbUser.Srole.ToString() == "普通用户")
                    {
                        Session["UserId"] = dbUser.UserId;
                        return RedirectToAction("Index", "User", new { id = dbUser.UserId, email = dbUser.Email.ToString() });
                    }


                }

            }

            ModelState.AddModelError("", "用户名或密码错误");
            return View(user);


        }
        public ActionResult Index()
        {
            return View();
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}