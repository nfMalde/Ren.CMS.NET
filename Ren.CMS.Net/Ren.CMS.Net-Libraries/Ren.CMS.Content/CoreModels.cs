using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Ren.CMS.CORE.Language;
using Ren.CMS.CORE.Links;
using Ren.CMS.nModules.Helper;
namespace Ren.CMS.Models.Core
{

     public class SendMailModel
     {
         public string Subject { get; set; }
         public string Body { get; set; }
         public int MailID { get; set; }
         public string Receiptient { get; set; }
         

     
     }
    public class nContentPostModel
    {
    
        public int ReferenceID {
            get;
            set;
        
        
        }
        
        //Eigenschaften
        /// <summary>
        /// Returns the ID of the Content
        /// </summary>
        /// 
        public int ID
        {
            get;
            set;
        }



        /// <summary>
        /// Returns the Title of the Content
        /// </summary>
        /// 
        [Required]
        public string Title
        {

            get;
            set;

        }
        /// <summary>
        /// Returns the Content Type of the Content
        /// </summary>
        /// 
        [Required]
        public string ContentType
        {

            get;
            set;

        }
        /// <summary>
        /// Returns the Category object
        /// </summary>
        /// 
        [Required]
        public string CategoryID
        {
            get;
            set;

        }

      
    
        /// <summary>
        /// Returns the Creators PK ID
        /// </summary>
        ///
        [Required]
        public string CreatorPKID
        {


            get;
            set;


        }

        /// <summary>
        /// Returns the Username of the Creator
        /// </summary>
        public string CreatorName
        {

            get;
            set;


        }

        public string SEOName { get; set; }


        public DateTime CreationDate
        {

            get;
            set;

        }

        public string CreatorSpecialName { get; set; }
        /// <summary>
        /// Returns Boolean of the Locked Status of the Content
        /// </summary>
        ///  
        
        public bool Locked
        {

            get;
            set;

        }
        /// <summary>
        /// Returns the COMMA SEPERATED Meta-Keywords
        /// </summary>
        /// 
       
        public string MetaKeyWords
        {

            get;
            set;


        }
        /// <summary>
        /// Returns the MetaDescription
        /// </summary>
        /// 
        [MaxLength(250)]
   
        public string MetaDescription
        {

            get;
            set;


        }
        /// <summary>
        /// Returns the PreviewText
        /// </summary>
        /// 
        [Required]
        
        [MaxLength(400)]
        public string PreviewText
        {

            get;
            set;


        }
        /// <summary>
        /// Returns the Content Long Text
        /// </summary>
        ///
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string LongText
        {

            get;
            set;


        }



        public int[] Tags { get; set; }
    
    }

    public class FlexyGridPostParameters
    {
        [Required]
        public int page { get; set; }
        [Required]
        public int rp { get; set; }
        [Required]
        public string sortname { get; set; }
        [Required]
        public string sortorder { get; set; }
        
        public string query { get; set; }

        public string qtype { get; set; }


    }


    public class BackendShortcut
    {   [Required]
        public int id {get;set;}

        public string IconUrl { get; set; }
        
        public string ShortCutText { get; set; }
        
        [Required]
        public string PosX { get; set; }
        [Required]
        public string PosY { get; set; }
        
        public string action { get; set; }

    }

    public class nSliderModel {

        public List<Ren.CMS.Content.nContent> ContentCollection { get; set; }
        public int SliderSize { get; set; }
    

    
    
    
    }
    public class SocialSharing {
        private int _width = 0;
        private int _height = 0;

        public string[] AddThisButtons { get; set; }
        public int width {
            get {

                return this._width;

            
            }
            set {

                this._width = value;
            
            }
        }
        public int height
        {
            get
            {

                return this._height;


            }
            set
            {

                this._height = value;

            }
        }
        public string ContainerClassesCSS { get; set; }
        public string ContainerStyle { get; set; }

    }
    public class nContentIncludedModel {


        public Ren.CMS.Content.nContent Entry
        {

            get;
            set;

        }
    
    
    
    }


    public class MenuModel
    {
       [Required]
        public LinkCollection Links
        {

            get;
            set;


        }



       public string getCorrectCSSClass(ControllerContext CTT, Link Lnk)
       {


            if (isLinkAcitve(CTT, Lnk))
            {

                return Lnk.HoverStateClass;

            }
            else {

                return Lnk.NormalStateClass;
            
            }
        
        
        }

        public bool isLinkAcitve(ControllerContext CTT, Link Lnk)
        {
           ControllerContext CT = CTT;
          
           string currentC = CT.RouteData.GetRequiredString("controller").ToLower();
           string currentA = CT.RouteData.GetRequiredString("action").ToLower();
           bool isok = false;             
           //Stripping Action
           if (currentC == Lnk.TargetController.ToLower()) isok = true;
           else isok = false;

           if (!isok) return isok;
           if (String.IsNullOrEmpty(Lnk.TargetAction) || String.IsNullOrWhiteSpace(Lnk.TargetAction) || Lnk.TargetAction == "*")
           {

               isok = true;

           }

           else {


               if (currentA == Lnk.TargetAction.ToLower())
               {


                   isok = true;


               }
               else {


                   isok = false;
               
               }
           
           
           }



    

           return isok;
       }
       
        
    }
   
 
}
