using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class TSettingValue {
        public virtual int Id { get; set; }
        public virtual string SettingValue { get; set; }
        public virtual int SettingID { get; set; }
    }
}
