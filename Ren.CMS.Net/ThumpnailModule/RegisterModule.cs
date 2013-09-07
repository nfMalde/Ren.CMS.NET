using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib;
using MvcContrib.PortableAreas;
using Ren.CMS.CORE.SqlHelper;
using Ren.CMS.ViewEngine;

namespace ThumpnailModule.Thumpnail
{
    public class ThumpnailRegistration:PortableAreaRegistration
    {

        private string ThumpnailPath = HttpContext.Current.Server.MapPath("~/StorageThumpnails");

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context, IApplicationBus bus)
        {



            string[] thisNamespace = { "ThumpnailModule.Thumpnail.Controllers" };
            #region Adding Routes
            context.MapRoute("Thumpnail",
                            "Thumpnail/{contentid}/{pkid}/{filename}-{Width}-{Height}.{ext}",
                            new { controller = "Thumpnail", action = "_Get", contentid = 0, pkid = "", filename = "", Width = 64, Height = 64, ext = "jpg" },
                            thisNamespace);
            context.MapRoute("Thumpnail-Smart",
                                "Thumpnail/{contentid}/{pkid}/{filename}.{ext}",
                                new { controller = "Thumpnail", action = "_Get", contentid = 0, pkid = "", filename = "", Width = 64, Height = 64, ext = "jpg" },
                                thisNamespace);

        

            context.MapRoute("Thumpnail-Minimal",
                                "Thumpnail",
                                new { controller = "Thumpnail", action = "_Get", contentid = 0, pkid = "", filename = "", Width = 64, Height = 64, ext = "jpg" },
                                thisNamespace);
            #endregion Adding Routes

            #region Install Module
            this.Install();
            #endregion Install Module


            RegisterAreaEmbeddedResources();
        }


        private void Install()
        {
            if (!Directory.Exists(ThumpnailPath))
            {
                Directory.CreateDirectory(ThumpnailPath);
            }

            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string prefix = new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix;
            SqlDataReader EX = SQL.SysReader("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@tb",
                                             new nSqlParameterCollection() { { "@tb", prefix + "Thumpnails_Module" } });

            if (!EX.HasRows)
            {
                EX.Close();

                StringBuilder Builder = new StringBuilder();
                Builder.AppendLine("CREATE TABLE " + prefix + "Thumpnails_Module (");
                Builder.AppendLine("id int NOT NULL IDENTITY(1,1),");
                Builder.AppendLine("atID varchar(255) NOT NULL,");
                Builder.AppendLine("LastModification datetime NOT NULL,");
                Builder.AppendLine("Path varchar(255),");
                Builder.AppendLine("Width int DEFAULT 64,");
                Builder.AppendLine("Height int DEFAULT 64");
                Builder.AppendLine(")");

                SQL.SysNonQuery(Builder.ToString(), new nSqlParameterCollection());
            
            }

            if (!EX.IsClosed)
                EX.Close();


            SQL.SysDisconnect();

        
        }

        public override string AreaName
        {
            get { return "Thumpnail"; }
        }
    }
}