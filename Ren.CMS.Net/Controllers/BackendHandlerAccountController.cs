using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using nfCMS_NET.Models.Backend.Account;
using nfCMS_NET.CORE.Permissions;
namespace nfCMS_NET.App_Data
{
    public class BackendHandlerAccountController : Controller
    {
        //
        // GET: /BackendHandlerAccount/

        public ActionResult Index()
        {



            return View();
        }


        

        [HttpPost]
        public JsonResult Logout()
        {

            FormsAuthentication.SignOut();

            return Json(new {success=true});
       
        }


        [HttpPost]
        public JsonResult LoggedIn()
        {
            
             
            return Json(new {

                LoggedIn = new MemberShip.nProvider.CurrentUser().isGuest()
            
            
            });
        
        
        }

        [HttpPost]
        public JsonResult Login(Login Model)
        {
            if (!ModelState.IsValid)
            {

                return Json(
                     new
                     {
                         loginData = new
                         {
                             hasPermission = false,
                             loginSuccessfull = false,
                             username = "Guest",
                             session = ""
                         }
                     });

            }
            else 
            {

                bool loginSuecc = false;
                bool hasPerm = false;
                MembershipUser User = null;

                if (Membership.ValidateUser(Model.uName, Model.uPassword))
                {

                    User = Membership.GetUser(Model.uName);
                    loginSuecc = true;
                    FormsAuthentication.SetAuthCookie(User.UserName, false);
                }
                

                nPermissions P = new nPermissions();

                hasPerm = P.hasPermission("USR_CAN_VIEW_BACKEND");
                string uname = 
                    (User != null ? User.UserName : "Guest");


                return Json(
                  new
                  {
                      loginData = new
                      {
                          hasPermission = hasPerm,
                          loginSuccessfull = loginSuecc,
                          username = uname,
                          session = "new"
                      }
                  });
            
            }
        
        
        }

        [HttpPost]
        public JsonResult CheckPermission()
        {

            nPermissions Perm = new nPermissions();

           
            return Json(
                new
                {

                    loginData = new { hasPermission = Perm.hasPermission("USR_CAN_ENTER_BACKEND") }


                }


                );

        
        }
    }
}
