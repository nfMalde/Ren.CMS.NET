using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.Persistence.Domain {
    
    public class TbTasks {

        public virtual int Id { get; set; }
        public virtual int Taskid { get; set; }
        public virtual bool Running { get; set; }
        public virtual string Taskname { get; set; }
        public virtual string Currentaction { get; set; }
        public virtual string Moduledbtable { get; set; }
        public virtual string Moduledbidentifier { get; set; }
        public virtual string Moduledbidvalue { get; set; }
        public virtual decimal Percentage { get; set; }
    }
}
