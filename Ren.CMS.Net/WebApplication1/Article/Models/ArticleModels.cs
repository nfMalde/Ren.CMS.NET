using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using nCMS_NET.CORE.Language;
using System.ComponentModel;
namespace nCMS_NET.Article.Models
{


    public class ArticleComment {
        [Required] 
        public string text { get; set; }
  
         
        public string nickname { get; set; }
    
    
    
    
    }


    public class ArticleListModel
    {
         
        
       
        
        public string Title { get; set; }
        public string ArticleListImage { get; set; }

 
        public string PreviewText { get; set; }
 
        public string SEOLink { get; set; }
        
        public string Author { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

      
    }
}
