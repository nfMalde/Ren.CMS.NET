using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class GlobalSetting {
        public virtual int Id { get; set; }
        public virtual string SettingName { get; set; }
        public virtual string SettingValue { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string SType { get; set; }
    }
}
