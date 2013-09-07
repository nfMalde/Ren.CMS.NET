using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ren.CMS.CORE.MenuBuilder
{

    public class Menu
    {
        private string viewNameContainer = null;
        private string viewNameItem = null;
        private ViewContext pvt = new ViewContext();
        public Menu(ViewContext VT, string viewnameContainer = "main_menu_partial.cshtml")
        {

            this.viewNameContainer = viewnameContainer;
            this.pvt = VT;


        }


        private string RenderPartialViewToString(string viewName, object model)
        {
            ViewDataDictionary ViewData = pvt.ViewData;
            TempDataDictionary TempData = pvt.TempData;
            ControllerContext CT = pvt.Controller.ControllerContext;


            if (string.IsNullOrEmpty(viewName))
                viewName = CT.RouteData.GetRequiredString("action");

            ViewData.Model = model;
            Ren.CMS.ViewEngine.nTheming Engin = new Ren.CMS.ViewEngine.nTheming();

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = Engin.FindPartialView(CT, viewName, false);
                ViewContext viewContext = new ViewContext(CT, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        private Links.LinkCollection getAllSublinks(int id)
        {

            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();

            Sql.SysConnect();
            Links.LinkCollection Ret = new Links.LinkCollection();

            string query = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links WHERE LinkIsActive='1' AND SublinkFrom=@ID";
            SqlParameter[] PP = new SqlParameter[] { new SqlParameter("@ID", id) };



            SqlDataReader L = Sql.SysReader(query, PP);
            while (L.Read())
            {


                Links.Link Lnk = new Links.Link((int)L["id"], (string)L["LinkType"], (string)L["LinkController"], (string)L["LinkAction"], (string)L["LinkText"], (string)L["LinkHref"],

                      (L["NormalStateClass"] != DBNull.Value ? (string)L["NormalStateClass"] : ""),
                        (L["HoverStateClass"] != DBNull.Value ? (string)L["HoverStateClass"] : ""));


                if (this.hasSublinks(Lnk.ID))
                {





                    Lnk.SubLinks = this.getAllSublinks(Lnk.ID);



                }



                Ret.Add(Lnk);


            }
            L.Close();
            Sql.SysDisconnect();
            return Ret;

        }


        private bool hasSublinks(int id)
        {


            string query = "SELECT COUNT(*) as c FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links WHERE LinkIsActive='1' AND SublinkFrom=@ID";
            SqlParameter[] PP = new SqlParameter[] { new SqlParameter("@ID", id) };


            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();
            SqlDataReader L = Sql.SysReader(query, PP);
            L.Read();


            int count = (int)L["c"];
            L.Close();
            Sql.SysDisconnect();
            if (count > 0) return true;
            else return false;




        }
        public HtmlString buildMenuCaptions(string identifierName = "*")
        {

            Language.Language LNG = new Language.Language("__USER__");
            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();
            MembershipUser User = new MemberShip.nProvider.CurrentUser().nUser;

            Settings.UserSettings usr = new Settings.UserSettings(User);

            object theme = usr.getSetting("USR_SETTING_THEME").Value;
            if (theme == null || theme == "")
            {

                theme = "nftheme";




            }

            string _ret = "";

            SqlHelper.nSqlParameterCollection ColSQL = new SqlHelper.nSqlParameterCollection();






            ColSQL.Add(new SqlParameter("@theme", theme));
            string query = "";

            if (identifierName != "*")
            {
                ColSQL.Add(new SqlParameter("@LinkType", identifierName));
                query = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links INNER JOIN " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers ON(" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.id = " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers.linkID) INNER JOIN " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers ON(" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.id=" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers.identifierID)  WHERE " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.LinkIsActive='1' AND " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.identiferName=@LinkType AND " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.SublinkFrom='' AND  " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.theme=@theme";
            }
            else
            {


                query = "SELECT * FROM " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links INNER JOIN " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers ON(" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.id = " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers.linkID) INNER JOIN " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers ON(" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.id=" + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links2Identfiers.identifierID)  WHERE " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.LinkIsActive='1' AND " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Links.SublinkFrom='' AND  " + (new ThisApplication.ThisApplication().getSqlPrefix) + "Link_Identifiers.theme=@theme";


            }

            SqlDataReader L = Sql.SysReader(query, ColSQL);
            Links.LinkCollection LCOL = new Links.LinkCollection();

            while (L.Read())
            {

                Links.LinkCollection SUBCOL = new Links.LinkCollection();


                Links.Link Lnk = new Links.Link((int)L["id"], (string)L["LinkType"], (string)L["LinkController"], (string)L["LinkAction"], (string)L["LinkText"], (string)L["LinkHref"],

                 (L["NormalStateClass"] != DBNull.Value ? (string)L["NormalStateClass"] : ""),
                   (L["HoverStateClass"] != DBNull.Value ? (string)L["HoverStateClass"] : ""));


                if (this.hasSublinks(Lnk.ID))
                {





                    Lnk.SubLinks = this.getAllSublinks(Lnk.ID);






                }
                LCOL.Add(Lnk);




            }


            L.Close();
            Sql.SysDisconnect();
            Ren.CMS.Models.Core.MenuModel MDL = new Models.Core.MenuModel();

            MDL.Links = LCOL;
            _ret = this.RenderPartialViewToString(this.viewNameContainer, MDL);
            return new HtmlString(_ret);
        }





    }





}
