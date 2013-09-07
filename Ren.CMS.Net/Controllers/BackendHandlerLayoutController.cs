using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nfCMS_NET.Models.Backend.Layout;
using nfCMS_NET.CORE.SqlHelper;
using nfCMS_NET.CORE.ThisApplication;
using nfCMS_NET.CORE.Permissions;
using System.Data.SqlClient;
using nfCMS_NET.MemberShip;
using System.Web.Security;
using nfCMS_NET.CORE.Language;
namespace nfCMS_NET.Controllers
{
    public class BackendHandlerLayoutController : Controller
    {
        //
        // GET: /BackendHandlerLayout/

        public ActionResult Index()
        {
            return Content("");
        }


        private bool hasSubmenu(int id)
        {
            bool hasIt = false;
            SqlHelper SQL = new SqlHelper();
            ThisApplication TA = new ThisApplication();
            string prefix = TA.getSqlPrefix;
            SQL.SysConnect();


            string query = "SELECT id,menuTextLang,action neededPermission FROM " + prefix + "Backend_Menu WHERE headID = @id";
            nSqlParameterCollection Pcol = new nSqlParameterCollection();

            Pcol.Add("@id", id);

            SqlDataReader Rows = SQL.SysReader(query, Pcol);

            hasIt = Rows.HasRows;

            Rows.Close();
            SQL.SysDisconnect();

            return hasIt;

        }
        private string getLI(int fromID)
        {
            SqlHelper SQL = new SqlHelper();
            ThisApplication TA = new ThisApplication();
            string prefix = TA.getSqlPrefix;
            SQL.SysConnect();

            nPermissions perm = new nPermissions();
            string query = "SELECT id,menuTextLang,action, neededPermission FROM " + prefix + "Backend_Menu WHERE headID = @id";
            nSqlParameterCollection Pcol = new nSqlParameterCollection();

            Pcol.Add("@id", fromID);
            Language Lang = new Language("__USER__","BACKEND_MENU");


            SqlDataReader Rows = SQL.SysReader(query, Pcol);
            string LIST = "";
           
            if(Rows.HasRows)
            while (Rows.Read())
            {

                if (perm.hasPermission((string)Rows["neededPermission"]))
                {
                    string langLine = Lang.getLine((string)Rows["menuTextLang"]);


                    string text = (langLine != "" ? langLine : (string)Rows["menuTextLang"]);
                    LIST += "<li>";
                    LIST +="<a href=\"javascript: new widgetAction('" + (string)Rows["action"] + "')\">" + text + "</a>";

                    if (this.hasSubmenu((int)Rows["id"]))
                    {
                        LIST += "<ul>";
                        LIST += this.getLI((int)Rows["id"]);
                        LIST += "</ul>";
                        
                    
                    
                    }
                    LIST += "</li>";

                
                
                
                }
             

            };

            Rows.Close();

            SQL.SysDisconnect();


            return LIST;

        }

        
        [HttpGet]
        public ActionResult Menu()
        {


            MenuModel MDL = new MenuModel();

            MDL.UserName = new nProvider.CurrentUser().nUser.UserName;
            MDL.LiElements = this.getLI(0);

            return View("BackendMenu", MDL);
        }


        [HttpPost]
        public JsonResult MenuCount()
        {
            nPermissions UserPermission = new nPermissions();
            SqlHelper Sql = new SqlHelper();
            Sql.SysConnect();
            ThisApplication TA = new ThisApplication();
            string prefix = TA.getSqlPrefix;
            string query = "SELECT id,neededPermission FROM " + prefix + "Backend_Menu";
            SqlDataReader Menu = Sql.SysReader(query);
            int count = 0;
            
            if (Menu.HasRows)
            {
                while (Menu.Read())
                {
                    if (Menu["neededPermission"] != DBNull.Value)
                   {
                       if (UserPermission.hasPermission((string)Menu["neededPermission"]))
                        {   
                           count++;
                        }
                   }
                }
            
            
            }
            Menu.Close();

            Sql.SysDisconnect();


            return Json( new { count = count } );
        
        
        }


        [HttpGet]
        public ActionResult LoginForm()
        {


            return View("LoginForm");
        }


        [HttpGet]
        public ActionResult Desktop()
        {
            DesktopModel Mdl = new DesktopModel();
            Mdl.MenuBarTime = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute;
            Mdl.MenuBarTimeTitle = DateTime.Now.ToShortDateString();

            return View("Desktop", Mdl);
        
        }
        [HttpPost]
        public JsonResult TimeUpdate()
        {

            return Json(new { 
            
            date = DateTime.Now.ToShortDateString(),
            time = DateTime.Now.Hour +":"+ DateTime.Now.Minute

            
            
            
            });
        
        }
    }
}
