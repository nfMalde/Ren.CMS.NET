using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class FileSettingValue {
        public virtual int Id { get; set; }
        public virtual int ProfileID { get; set; }
        public virtual int SettingID { get; set; }
        public virtual string SettingValue { get; set; }
    }
}
