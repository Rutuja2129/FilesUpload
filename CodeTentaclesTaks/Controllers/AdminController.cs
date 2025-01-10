using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using CodeTentaclesTaks.Models;

namespace CodeTentaclesTaks.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin a)
        {
            try
            {
                if (a.UserName == "rutujaupare11223@gmail.com" && a.Password == "Ruuuuu@21")
                {
                    Session["Uname"] = a.UserName;
                    return RedirectToAction("AddProduct", "Product");

                }
                else
                {
                    ViewData["Message"] = "Incorrect username or password";
                    return RedirectToAction("Login", "Admin");
                }
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
                return RedirectToAction("Login", "Admin");
            }

        }
    }
}