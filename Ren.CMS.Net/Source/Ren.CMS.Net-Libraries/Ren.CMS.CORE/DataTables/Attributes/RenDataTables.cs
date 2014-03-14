using Mvc.JQuery.Datatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.DataTables.Attributes
{
       public class RenDataTablesAttribute : Attribute
    {
       

        public RenDataTablesAttribute()
        {
      
        }

        public string LanguageLine
        {
            get;
            set;
        }

        public string LanguagePackage
        {
            get;
            set;
        }

 

        private string _displayName = null;

        public string DisplayName
        {
            get
            {
                string ret = null;
                if (this.LanguageLine != null)
                    ret = new Language.Language("__USER__", (this.LanguagePackage != null ? this.LanguagePackage : "Root")).getLine(this.LanguageLine);

                if (ret == String.Empty)
                    ret = this._displayName;


                return ret;
            }

            set
            {
                this._displayName = value;
            }

        }




   
}
}
