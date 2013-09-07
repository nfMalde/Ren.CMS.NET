using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ArticleRating {
        public virtual int Id { get; set; }
        public virtual int ArticleID { get; set; }
        public virtual int RatingID { get; set; }
        public virtual int Stars { get; set; }
    }
}
