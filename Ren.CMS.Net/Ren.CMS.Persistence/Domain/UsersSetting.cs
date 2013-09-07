using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {


    public class UsersSetting {
        public virtual int Id { get; set; }
        public virtual string Upkid { get; set; }
        public virtual string SettingName { get; set; }
        public virtual string SettingValue { get; set; }
    }
}
