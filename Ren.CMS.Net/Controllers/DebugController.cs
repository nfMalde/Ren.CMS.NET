using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nfCMS_NET.CORE.Settings;
using nfCMS_NET.CORE.SqlHelper;

namespace nfCMS_NET.Controllers
{
    public class UserTest {
        //required model properties
        public string TableName { get; set; }
        public string IdentityName { get; set; }
        public object IdentityValue { get; set; }


        public object PKID { get; set; }
        public string UserName { get; set; }
    
    }
    public class DebugController : Controller
    {
        //
        // GET: /Debug/
        public ActionResult Test() {

            SqlHelper Sql = new SqlHelper();

            Sql.SysConnect();

            UserTest Usr = new UserTest();
            Usr.TableName = "Users";
            Usr.IdentityName = "UserName";
            Usr.IdentityValue = "Malte";
            Usr = Sql.getModel(Usr);
            string usrname = Usr.UserName;

            return Content(usrname);
        
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserSettings() {
            
            UserSettings Settings = new UserSettings(new MemberShip.nProvider.CurrentUser().nUser);

            ViewData["Settings"] = Settings.listSettings(true);

            return View();
        
        }


        public ActionResult GlobalSettings()
        {

            GlobalSettings Settings = new GlobalSettings();

            ViewData["Settings"] = Settings.listSettings(true);

            return View();

        }
        public ActionResult AddGlobalSetting()
        {

            string name = Request.Form["name"].ToString();
            string dval = Request.Form["dval"].ToString();
            string catname = Request.Form["catname"].ToString();
            string stype = Request.Form["stype"].ToString();
            string vtype = Request.Form["vtype"].ToString();
            string frontend = Request.Form["frontend"].ToString();
            string backend = Request.Form["backend"].ToString();
            string label = Request.Form["label"].ToString();
            string descr = Request.Form["descr"].ToString();


            GlobalSettings USR = new GlobalSettings();

            nSetting S = new nSetting();
            nValueType VT = new nValueType();



            S.Name = name;


            if (vtype == VT.ValueArray)
            {
                S.DefaultValue = dval.Split(';');

            }
            else
                S.DefaultValue = dval;

            S.CategoryName = catname;
            S.SettingType = stype;
            S.ValueType = vtype;

            S.PermissionBackend = backend;
            S.PermissionFrontend = frontend;
            S.LabelLanguageLine = null;
            S.DescriptionLanguageLine = null;
            S.Label = label;
            S.Description = descr;
            USR.AddSetting(S);

            return RedirectToAction("GlobalSettings");
        }

        public ActionResult RegisterFile() {



            return View();
        }

        
        
        
        public ActionResult RegisterFile(HttpPostedFile datei) {

            nfCMS_NET.CORE.FileManagement.FileManagement FM = new CORE.FileManagement.FileManagement();

            nfCMS_NET.CORE.FileManagement.nFile newF = new CORE.FileManagement.nFile();

          

            return RedirectToAction("RegisterFile", "Debug");
        }

        public ActionResult AddUserSetting() {

            string name = Request.Form["name"].ToString();
            string dval = Request.Form["dval"].ToString();
            string catname = Request.Form["catname"].ToString();
            string stype = Request.Form["stype"].ToString();
            string vtype = Request.Form["vtype"].ToString();
            string frontend = Request.Form["frontend"].ToString();
            string backend = Request.Form["backend"].ToString();
            string label = Request.Form["label"].ToString();
            string descr = Request.Form["descr"].ToString();


            UserSettings USR = new UserSettings(new nfCMS_NET.MemberShip.nProvider.CurrentUser().nUser);

            nSetting S = new nSetting();
            nValueType VT = new nValueType();
            
            
          
            S.Name = name;


            if (vtype == VT.ValueArray)
            {
                S.DefaultValue = dval.Split(';');

            }
            else
                S.DefaultValue = dval;

            S.CategoryName = catname;
            S.SettingType = stype;
            S.ValueType = vtype;
          
            S.PermissionBackend = backend;
            S.PermissionFrontend = frontend;
            S.LabelLanguageLine = null;
            S.DescriptionLanguageLine = null;
            S.Label = label;
            S.Description = descr;
            USR.AddSetting(S);
         
            return RedirectToAction("UserSettings");
        }
    }
}
