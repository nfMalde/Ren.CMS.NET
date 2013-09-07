using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class BackendWidget {
        public virtual int Id { get; set; }
        public virtual string WidgetName { get; set; }
        public virtual string WidgetPartialView { get; set; }
        public virtual string NeededPermission { get; set; }
        public virtual System.Nullable<int> DefinedWidth { get; set; }
        public virtual System.Nullable<int> DefinedHeight { get; set; }
        public virtual string Icon { get; set; }
    }
}
