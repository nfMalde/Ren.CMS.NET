using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class SettingStores2Locale {
        public virtual int Id { get; set; }
        public virtual int Stid { get; set; }
        public virtual string LangLine { get; set; }
    }
}
