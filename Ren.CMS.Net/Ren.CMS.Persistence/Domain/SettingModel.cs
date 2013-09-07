using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class SettingModel {
        public virtual int Id { get; set; }
        public virtual string SettingName { get; set; }
        public virtual string SettingLangLineLabel { get; set; }
        public virtual string SettingLangLineDescr { get; set; }
        public virtual string SettingDefaultValue { get; set; }
        public virtual string SettingRelation { get; set; }
        public virtual string SettingType { get; set; }
        public virtual string ValueType { get; set; }
        public virtual System.Nullable<int> Cid { get; set; }
    }
}
