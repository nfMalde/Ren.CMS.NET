using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvc.JQuery.Datatables;
using Mvc.JQuery.Datatables.Serialization;
using Mvc.JQuery.Datatables.Models;
using Ren.CMS.CORE.DataTables.Attributes;
using Ren.CMS.CORE.Permissions;
using System.Web.Script.Serialization;
namespace Ren.CMS.CORE.DataTables.BackendModels
{
    public class ContentListView
    {

        [DataTables(Visible= false)]
        public int ID { get; set; }
        [RenDataTables(LanguageLine="LANG_CONTENT_TITLE", LanguagePackage="CONTENT_MANAGEMENT")]
        public string Title { get; set; }
        
      //  [DataTables(DisplayName = Language.LanguageDefaults.LanguageDefaultsContent.LANG_CONTENT_CREATOR.ReturnLangLine())]
        [RenDataTables(LanguageLine = "LANG_CONTENT_CREATOR", LanguagePackage = "CONTENT_MANAGEMENT")]
        public string Creator { get; set; }

       // [DataTables(DisplayName = Language.LanguageDefaults.LanguageDefaultsContent.LANG_CONTENT_CREATOR.ReturnLangLine())]
        [RenDataTables(LanguageLine = "LANG_CONTENT_CATEGORY", LanguagePackage = "CONTENT_MANAGEMENT")]
        public string Category { get; set; }

       // [DataTables(DisplayName = Language.LanguageDefaults.LanguageDefaultsContent.LANG_CONTENT_CREATION_DATE.ReturnLangLine())]
        [RenDataTables(LanguageLine = "LANG_CONTENT_CREATION_DATE", LanguagePackage = "CONTENT_MANAGEMENT")]
        public DateTime cDate { get; set; }

        [DataTables(Visible=false)]
        public string ContentType { get; set; }

        //[DataTables(DisplayName = Language.LanguageDefaults.LanguageDefaultsContent.LANG_CONTENT_TYPE.ReturnLangLine())]
        [RenDataTables(LanguageLine = "LANG_CONTENT_TYPE", LanguagePackage = "CONTENT_MANAGEMENT")]
        public string ContentTypeText
        {
            get
            {
                if (this.ContentType != null)
                {
                    Ren.CMS.CORE.Language.Language Lang = new Ren.CMS.CORE.Language.Language("__USER__", "CONTENT_TYPES");

                    string ctLangLine = Lang.getLine("LANG_CTYPE_" + this.ContentType.ToUpper());
                    if (String.IsNullOrEmpty(ctLangLine) || String.IsNullOrWhiteSpace(ctLangLine))
                        return this.ContentType;

                    return ctLangLine;
                }

                return String.Empty;
            }
        }


       [DataTables(MRenderFunction= "renderActionColumn", Sortable = false, DisplayName ="<span class=\"ActionColumn\"></span>") ]
       
        public string ActionColumn
        {
            get
            {
                object buttons = new
                {
                    delete = new { enabled = nPermissions.hasPermission("USR_CAN_DELETE_CONTENTS"), action = "alert(\"delete\");" },
                    edit = new { enabled = nPermissions.hasPermission("USR_CAN_EDIT_CONTENT"), action = "alert(\"edit\");" }

                };

                var json = new JavaScriptSerializer().Serialize(buttons);

                return json;

            }
        }


    }
}
