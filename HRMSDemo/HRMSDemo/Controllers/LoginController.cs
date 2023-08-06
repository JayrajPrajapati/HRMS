using ModelLibrary.Models;
using RepositoryLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMSDemo.Controllers
{
    /// <summary>
    /// LoginController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class LoginController : Controller
    {
        /// <summary>
        /// The iprospect services
        /// </summary>
        private readonly IProspectServices IprospectServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="prospectServices">The prospect services.</param>
        public LoginController(IProspectServices prospectServices)
        {
            IprospectServices = prospectServices;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()

        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Logins the specified user model.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = IprospectServices.LoginUsingUsernameandpaswword(userModel);
                    if (user != null)
                    {
                        Session["UserID"] = user.UserID.ToString();
                        Session["UserName"] = user.UserName.ToString();
                        Session["Email"] = user.Email.ToString();
                        if (user.UserID == 1)
                        {
                            return RedirectToAction("Index", "Prospect");
                        }
                        else
                        {
                            return RedirectToAction("IndexInterview", "Prospect");

                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The username or password is incorrect");
                    return View("Login");
                }

                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}