using Proje_.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proje_.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        ProductDal _productDal = new ProductDal();
        public ActionResult Index()
        {
            return View();
        }

       [HttpPost]
        public ActionResult Index(FormCollection fc)
        {


           int a= _productDal.Admin_Login(fc["admin_name"], fc["password"]);
            if (a==1)
            {
                FormsAuthentication.SetAuthCookie(fc["admin_name"], false);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewBag.LoginError = "Kullanıcı Adı veya Şifre yanlış.";
            }
            return View();
           

        }

    }
}