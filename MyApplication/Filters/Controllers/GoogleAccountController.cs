﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Filters.Controllers
{
    public class GoogleAccountController : Controller
    {
        // GET: GoogleAccount
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string password,string returnUrl)
        {
            if (username.EndsWith("@google.com") && password == "1")
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码不正确");
                return View();
            }
        }
    }
}