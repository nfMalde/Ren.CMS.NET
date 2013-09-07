using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ren.CMS.Models.Backend.Layout;
using Ren.CMS.CORE.SqlHelper;
using Ren.CMS.CORE.ThisApplication;
using Ren.CMS.CORE.Permissions;
using System.Data.SqlClient;
using Ren.CMS.MemberShip;
using System.Web.Security;
using Ren.CMS.CORE.Language;

namespace Ren.CMS.Controllers
{
    public class BackendHandlerLayoutController : Controller
    {
        //
        // GET: /BackendHandlerLayout/

        public ActionResult Index()
        {
            return Content("");
        }


        [HttpPost]
        public ActionResult Widget(WidgetPost Pos)
        {

            if (ModelState.IsValid)
            {
                ThisApplication TA = new ThisApplication();
                string prefix = TA.getSqlPrefix;

                SqlHelper SQL = new SqlHelper();
                SQL.SysConnect();
                string query = "SELECT id,Icon, widgetName, widgetPartialView, neededPermission,definedWidth, definedHeight FROM " + prefix + "Backend_Widgets WHERE widgetName=@name";
                nSqlParameterCollection PCol = new nSqlParameterCollection();
                PCol.Add("@name", Pos.widget);

                SqlDataReader widget = SQL.SysReader(query, PCol);
                WidgetReturn WR = new WidgetReturn();

                if (widget.HasRows)
                {
                    Language Lang = new Language("__USER__", "BACKEND_WIDGETS");


                    widget.Read();

                    WR.id = (int)widget["id"];
                    WR.neededPermission = (string)widget["neededPermission"];
                    WR.widgetName = (string)widget["widgetName"];
                    WR.widgetPartialView = (string)widget["widgetPartialView"];
                    string langName = "LANG_BACKEND_WIDGETS_" + WR.widgetName.ToUpper() + "_TITLE";
                    string langLine = Lang.getLine(langName);
                    WR.widgetTitle = (String.IsNullOrEmpty(langLine) ? langName : langLine);

                    if (widget["Icon"] != DBNull.Value)
                    {

                        WR.Icon = "/BackendFileHandler/Icons/" + (string)widget["Icon"];
                    }
                    else
                    {
                        WR.Icon = "/BackendFileHandler/Icons/Defaulticon.png";
                    }
                    if (widget["definedWidth"] != DBNull.Value)
                    {

                        WR.definedWidth = (int)widget["definedWidth"];

                    }
                    if (widget["definedHeight"] != DBNull.Value)
                    {

                        WR.definedHeight = (int)widget["definedHeight"];

                    }
                    
                }
                else 
                {

                    WR.id = -1;
                    WR.widgetName = "404_NOT_FOUND_XXXXXX";
                    WR.widgetPartialView = "WidgetErrors/404_not_found";
                        
                }
                widget.Close();

                SQL.SysDisconnect();

                if (!nPermissions.hasPermission(WR.neededPermission) && WR.id != -1)
                {

                    WR.id = -1;
                    WR.widgetName = "403_NO_ACCESS_XXXXXX";
                    WR.widgetPartialView = "WidgetErrors/accessDenied";
                    WR.neededPermission = null;
                }
                if (WR.definedWidth == 0)
                {

                    WR.definedWidth = 550;

                
                }
                if (WR.definedHeight == 0)
                {
                    WR.definedHeight = 400;
                }
                WR.widgetHeaderData = Pos.widgetHeaderData;
                    return View("widgets/widgetManager", WR);

                  
            }
            else
            {
                return Content("");
            }
        
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

             string query = "SELECT id,menuTextLang,action, neededPermission FROM " + prefix + "Backend_Menu WHERE headID = @id";
            nSqlParameterCollection Pcol = new nSqlParameterCollection();

            Pcol.Add("@id", fromID);
            Language Lang = new Language("__USER__","BACKEND_MENU");


            SqlDataReader Rows = SQL.SysReader(query, Pcol);
            string LIST = "";
           
            if(Rows.HasRows)
            while (Rows.Read())
            {

                if (nPermissions.hasPermission((string)Rows["neededPermission"]))
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


        [HttpPost]
        public ActionResult RenderIcon(Models.Core.BackendShortcut ShortCut)
        {




            return View("_Shurtcut", ShortCut);
        }

        [HttpPost]

        public JsonResult Icons()
        {
            SqlHelper Sql = new SqlHelper();
            nSqlParameterCollection Pcol = new nSqlParameterCollection();
            Sql.SysConnect();

            Pcol.Add("@user", new nProvider.CurrentUser().nUser.ProviderUserKey);
            ThisApplication TA = new ThisApplication();
            string prefix = TA.getSqlPrefix;


            string query = "SELECT i.id as id, i.langLine as langLine, ISNULL(u.Icon,i.Icon) as Icon,i.Action as Action, u.xPos as xPos, u.yPos as yPos FROM " + prefix + "Backend_Desktop_Icons i INNER JOIN " +
                            prefix + "Backend_User_Desktops u ON(u.iconID = i.id) WHERE u.userid=@user";
            SqlDataReader Icons = Sql.SysReader(query, Pcol);
            List<object> Ico = new List<object>();

            if (Icons.HasRows)
            {
                Language Lang = new Language("__USER__","DESKTOP_ICONS");


                while (Icons.Read())
                {

                    Ico.Add(
                        new
                        {
                            id = (int)Icons["id"],
                            text = Lang.getLine((string)Icons["langLine"]),
                            iconUrl = "/BackendFileHandler/Icons/" + (string)Icons["Icon"],
                            action = (string)Icons["Action"],
                            xPos = (double)Icons["xPos"],
                            yPos = (double)Icons["yPos"]

                        });
                
                }
            
            }
            Icons.Close();

            Sql.SysDisconnect();
            return Json(new { count = Ico.Count, icons = Ico });
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
                       if (nPermissions.hasPermission((string)Menu["neededPermission"]))
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
