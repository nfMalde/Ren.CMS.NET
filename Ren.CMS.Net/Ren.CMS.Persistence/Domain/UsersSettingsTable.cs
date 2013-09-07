using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class UsersSettingsTable {
        public virtual int Id { get; set; }
        public virtual string SettingName { get; set; }
        public virtual string SettingDefaultVal { get; set; }
        public virtual string SettingLangLine { get; set; }
        public virtual string SettingLongDescription { get; set; }
        public virtual System.Nullable<int> SettingOrder { get; set; }
        public virtual string DataType { get; set; }
        public virtual string SType { get; set; }
    }
}
