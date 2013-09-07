using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ren.CMS.Models.Backend.Account;
using Ren.CMS.CORE.Permissions;
using Ren.CMS.CORE.ThisApplication;
using Ren.CMS.CORE.SqlHelper;
using Ren.CMS.CORE.Language;
using Ren.CMS.Models;
namespace Ren.CMS.App_Data
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
        public ActionResult SaveDesktop(Models.Backend.Layout.changeBackground B)
        {
            //Get Current User

            MembershipUser User = new MemberShip.nProvider.CurrentUser().nUser;

            SqlHelper SQL = new SqlHelper();

            SQL.SysConnect();
            string prefix = new ThisApplication().getSqlPrefix;
            string check = "SELECT * FROM " + prefix + "Backend_Desktop_Backgrounds WHERE userid=@uid";
            nSqlParameterCollection CheckCol = new nSqlParameterCollection();
            CheckCol.Add("@uid", User.ProviderUserKey);

            System.Data.SqlClient.SqlDataReader C = SQL.SysReader(check, CheckCol);

            nSqlParameterCollection UpdateCol = new nSqlParameterCollection();
            UpdateCol.Add("@bimg", B.BGImage);
            UpdateCol.Add("@bcolor", B.Color);
            UpdateCol.Add("@balign", B.Align);
            UpdateCol.Add("@brepeat", B.Repeat);
            UpdateCol.Add("@uid", User.ProviderUserKey);


            if (C.HasRows)
            {

                C.Close();

                string update = "UPDATE " + prefix + "Backend_Desktop_Backgrounds SET backgroundImage=@bimg, backgroundColor=@bcolor, backgroundAlign=@balign, backgroundRepeat=@brepeat WHERE userid=@uid";
                SQL.SysNonQuery(update, UpdateCol);
            }
            else 
            {

                C.Close();
                string insert = "INSERT INTO " + prefix + "Backend_Desktop_Backgrounds (backgroundImage,backgroundColor,backgroundAlign,backgroundRepeat, userid) VALUES(@bimg,@bcolor,@balign,@brepeat,@uid)";

                SQL.SysNonQuery(insert, UpdateCol);
            
            }
            SQL.SysDisconnect();
            return Content("<span style=\"color:green\">Änderungen erfolgreich gespeichert</span>","text/html");
        }
        [HttpPost]
        public JsonResult Logout()
        {

            FormsAuthentication.SignOut();

            return Json(new {success=true});
       
        }

        [HttpPost]
        public JsonResult GetBGFiles()
        {

            List<object> Files = new List<object>();

            MembershipUser Cu = new MemberShip.nProvider.CurrentUser().nUser;
            string path = "/BackendFileHandler/CustomDesktops/" + Cu.ProviderUserKey;

            string[] files = System.IO.Directory.GetFiles(Server.MapPath(path));

            foreach (string file in files)
            {

                Files.Add(new { name = System.IO.Path.GetFileName(file) });
            
            
            }


            return Json(new { files = Files });
        }


        [HttpPost]
        public WrappedJsonResult SaveDesktopImage()
        {

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase Picture = Request.Files[0];
                if (nPermissions.hasPermission("USR_CAN_ENTER_BACKEND"))
                {
                    if (Picture.ContentType.ToLower().StartsWith("image"))
                    {
                        MembershipUser Cu = new MemberShip.nProvider.CurrentUser().nUser;


                        string path = "/BackendFileHandler/CustomDesktops/" + Cu.ProviderUserKey + "/";
                        int x = 1;
                        string fname = System.IO.Path.GetFileName(Picture.FileName);
                        string fnameReal = System.IO.Path.GetFileName(Picture.FileName);

                        string ext = System.IO.Path.GetExtension(Picture.FileName);
                        while (System.IO.File.Exists(Server.MapPath(path + fname)))
                        {
                            fname = "(" + x + ")" + fnameReal;
                            x++;

                        }
                        string pathFinal = Server.MapPath(path + fname);
                        Picture.SaveAs(pathFinal);
                        pathFinal = "/BackendFileHandler/CustomDesktops/" + Cu.ProviderUserKey + "/" + fname;
                        
                        return new WrappedJsonResult
                            {
                                Data = new
                                {
                                    IsValid = true,
                                    Message = "",
                                    ImagePath = pathFinal
                                }
                            };

                    }
                    else
                    {

                        return new WrappedJsonResult
                          {
                              Data = new
                              {
                                  IsValid = false,
                                  Message = "Error: Wrong Filetype",
                                  ImagePath = ""
                              }
                          };

                    }
                }
                return new WrappedJsonResult
                {
                    Data = new
                    {
                        IsValid = false,
                        Message = "Error: No Permission",
                        ImagePath = ""
                    }
                };
            }

            return new WrappedJsonResult
            {
                Data = new
                {
                    IsValid = false,
                    Message = "Error: No File",
                    ImagePath = ""
                }
            };
            }
        
        
        
        
        
        
     


        [HttpPost]
        public ActionResult AddIcon(Models.Core.BackendShortcut S)
        {
            MembershipUser U = new MemberShip.nProvider.CurrentUser().nUser;

            if (new MemberShip.nProvider.CurrentUser().isGuest() || !ModelState.IsValid) return Content("");
            Ren.CMS.CORE.SqlHelper.SqlHelper SQL = new CORE.SqlHelper.SqlHelper();
            Ren.CMS.CORE.ThisApplication.ThisApplication TA = new CORE.ThisApplication.ThisApplication();
                Ren.CMS.CORE.Language.Language Lang = new Ren.CMS.CORE.Language.Language("__USER__","DESKTOP_ICONS");

            string prefix = TA.getSqlPrefix;


            string queryCheck = "SELECT * FROM " + prefix + "Backend_User_Desktops WHERE iconID=@id AND userid=@uid";
            string insert = "INSERT INTO " + prefix + "Backend_User_Desktops(xPos,yPos,iconID,userid) VALUES(@x,@y,@iconID,@uid)";

            //create collections

            CORE.SqlHelper.nSqlParameterCollection PCOL1 = new CORE.SqlHelper.nSqlParameterCollection();
            PCOL1.Add("@id", S.id);
            PCOL1.Add("@uid", U.ProviderUserKey);

            CORE.SqlHelper.nSqlParameterCollection PCOL2 = new CORE.SqlHelper.nSqlParameterCollection();
            PCOL2.Add("@x", S.PosX);
            PCOL2.Add("@y", S.PosY);
            PCOL2.Add("@iconID", S.id);
            PCOL2.Add("@uid", U.ProviderUserKey);

            SQL.SysConnect();

            System.Data.SqlClient.SqlDataReader E = SQL.SysReader(queryCheck, PCOL1);

            if (E.HasRows)
            {


                E.Close();
                SQL.SysDisconnect();

                return Content("");
            }

            else 
            {

                E.Close();

                SQL.SysNonQuery(insert, PCOL2);


                string query = "SELECT i.id as id, i.langLine as langLine, ISNULL(u.Icon,i.Icon) as Icon,i.Action as Action, u.xPos as xPos, u.yPos as yPos FROM " + prefix + "Backend_Desktop_Icons i INNER JOIN " +
                                prefix + "Backend_User_Desktops u ON(u.iconID = i.id) WHERE u.userid=@user AND i.id=@id";
                CORE.SqlHelper.nSqlParameterCollection PCOL3 = new CORE.SqlHelper.nSqlParameterCollection();
                PCOL3.Add("@user", U.ProviderUserKey);
                PCOL3.Add("@id", S.id);

                if (!E.IsClosed) E.Close();

                System.Data.SqlClient.SqlDataReader SC = SQL.SysReader(query, PCOL3);

                if (!SC.HasRows)
                {
                    

                    SC.Close();
                    SQL.SysDisconnect();
                    return Content("");
                }
                else 
                {

                    SC.Read();

                    S.IconUrl = "/BackendFileHandler/Icons/" + (string)SC["Icon"];
                    S.ShortCutText = Lang.getLine((string)SC["langLine"]);
                    S.action = ((string)SC["Action"]);
                    SC.Close();
                    SQL.SysDisconnect();

                    return View("_Shurtcut", S);
                

                
                }

            }
        
        }
        [HttpPost]

        public JsonResult RemoveIcon(Models.Backend.Account.RemoveIcon I)
        {
            bool success = false;
        string errormsg = "";

            try
            {
            
              MemberShip.nProvider.CurrentUser CU = new MemberShip.nProvider.CurrentUser();

              if(CU.isGuest()) throw new Exception("Error: User is guest!");

             MembershipUser U = CU.nUser;
             if(!ModelState.IsValid)
                 throw new Exception("Error: Invalid Post!");

            CORE.SqlHelper.nSqlParameterCollection PCOL = new CORE.SqlHelper.nSqlParameterCollection();
            CORE.SqlHelper.SqlHelper SQL = new CORE.SqlHelper.SqlHelper();

            CORE.ThisApplication.ThisApplication TA = new CORE.ThisApplication.ThisApplication();

            string prefix = TA.getSqlPrefix;

            string query = "DELETE "+ prefix +"Backend_User_Desktops WHERE iconID=@id AND userid=@uid";

            PCOL.Add("@id",I.id);
            PCOL.Add("@uid", U.ProviderUserKey);
            
            SQL.SysConnect();

            SQL.SysNonQuery(query,PCOL);
            
            SQL.SysDisconnect();
            success = true;


            
            
            
            }
            catch(Exception e)
            {
                success = false;
                errormsg = e.Message;
            }

            return Json(new { success = success, errormsg = errormsg });
        
        
        }

        [HttpPost]
        public ActionResult UpdateIconPos(Models.Core.BackendShortcut S)
        {
            MembershipUser U = new MemberShip.nProvider.CurrentUser().nUser;
            Ren.CMS.CORE.SqlHelper.SqlHelper SQL = new CORE.SqlHelper.SqlHelper();
            CORE.SqlHelper.nSqlParameterCollection PCOL = new CORE.SqlHelper.nSqlParameterCollection();
            if (new MemberShip.nProvider.CurrentUser().isGuest()) return Content("USR.0");
            if (!ModelState.IsValid) return Content("FRM.0");

            double testVar = 0.0;

            if (!double.TryParse(S.PosY, out testVar)) return Content("Y.10");
            if (!double.TryParse(S.PosX, out testVar)) return Content("X.10");

            CORE.ThisApplication.ThisApplication TA = new CORE.ThisApplication.ThisApplication();

            string pref = TA.getSqlPrefix;


            SQL.SysConnect();
            string nonQuery = "UPDATE " + pref + "Backend_User_Desktops SET xPos=@x, yPos=@y WHERE iconID=@id AND userid=@uid";
            PCOL.Add("@x", S.PosX);
            PCOL.Add("@y", S.PosY);
            PCOL.Add("@id", S.id);
            PCOL.Add("@uid", U.ProviderUserKey);

            SQL.SysNonQuery(nonQuery, PCOL);


            SQL.SysDisconnect();


            return Content("");
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



                hasPerm = nPermissions.hasPermission("USR_CAN_VIEW_BACKEND");
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

            
           
            return Json(
                new
                {

                    loginData = new { hasPermission = nPermissions.hasPermission("USR_CAN_ENTER_BACKEND") }


                }


                );

        
        }
    }
}
